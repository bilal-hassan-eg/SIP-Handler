using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ozeki.VoIP;
using SIPManagementNewMVVM.Model.DataModel;
using SIPManagementNewMVVM.Model.DataHandel;
using System.Windows;

namespace SIPManagementNewMVVM.Model.OzekiModels
{
    internal class CallManyNumbers
    {
        public List<NumberModel> Numbers { get; set; }
        public int ThreadNumber { get; set; }
        public string userAgent { get; set; }
        public RegistManyAccounts RegistManyAccounts { get; set; }

        public delegate void CallStart(object sender, CallStateChangedArgs e);
        public event CallStart callStartEvent;

        public string RepeatMode { get; set; } = "ByTimer";
        public int TimeRepaet { get; set; } = 0;

        public int codeStop1 { get; set; }
        public int codeStopAll { get; set; }

        public bool Operation { get; set; } // false for HangUp call after finish timer true for DontHangUp and Transfer

        public CallManyNumbers(string userAgent)
        {
            RegistManyAccounts = new RegistManyAccounts(userAgent);
            RegistManyAccounts.registEvent += RegistrationAccount;
        }



        public void Call(IPhoneLine phoneLine , ISoftPhone softPhone)
        {
            try
            {
                List<NumberModel> numbers = new List<NumberModel>();
                foreach (NumberModel model in Numbers)
                {
                    numbers.Add(model);
                }
                // this copy data its so important to make list for each account 
                // if we dont do that it will call a number for only one time 
                // this method allow to us to call number just one time to same account 
                // but we can do call number more than 1 time to deffrent account HAVE FUN :]

                // in list nunber dont take finish call timer hangup call after accept 
                // in transfer mode take fiinsh call timer if mode = none after accept

                for (int i = 0; i < ThreadNumber; i++)
                {
                    Task.Run(
                        delegate ()
                        {
                            try
                            {
                                List<IPhoneCall> PhoneCalls = new List<IPhoneCall>();
                                for (int counter = 0; counter < numbers.Count; counter++)
                                {
                                    //MessageBox.Show($"ACC : {phoneLine.SIPAccount.RegisterName}");
                                    if (numbers[counter].isTaken) 
                                        continue;
                                    numbers[counter].isTaken = true;
                                    IPhoneCall call = softPhone.CreateCallObject(phoneLine, numbers[counter].PhoneNumber.Trim());
                                    PhoneCalls.Add(call);
                                    numbers[counter].CallID = call.CallID;
                                    var accountModel = numbers[counter].AccountModel.SIPAccount;
                                    call.CallStateChanged += delegate (object sender, CallStateChangedArgs e)
                                    {
                                        // export accounts & call to txt file
                                        switch (Operation)
                                        {
                                            case true:
                                                //logNumbers.Add(new Tuple<SIPAccount, string, int, NumberModel, string>(accountModel, "transfer_calls", e.StatusCode, numbers[counter], e.Reason));
                                                Export.ExportCall(accountModel, "transfer_calls", e.StatusCode, numbers[counter], e.Reason);
                                                break;
                                            case false:
                                                //logNumbers.Add(new Tuple<SIPAccount, string, int, NumberModel, string>(accountModel, "list_call", e.StatusCode, numbers[counter], e.Reason));
                                                Export.ExportCall(accountModel, "list_call", e.StatusCode, numbers[counter], e.Reason);
                                                break;
                                        }

                                        numbers[counter]._CallState = e.StatusCode;
                                        numbers[counter]._reason = e.Reason;
                                        numbers[counter].CallStates.Add(e.StatusCode);
                                        numbers[counter].CallReasons.Add(e.State.ToString());

                                        // stop for specific repsonse continue this call or cancel all calls

                                        if (e.StatusCode == codeStop1 && Operation == true)
                                        {
                                            call.HangUp();
                                            counter++;
                                        }
                                        else if (e.StatusCode == codeStopAll && Operation == true)
                                        {
                                            foreach (NumberModel number in numbers)
                                            {
                                                number.isTaken = true;
                                            }
                                        }

                                        if (e.StatusCode == 200 && e.State == Ozeki.VoIP.CallState.InCall && Operation == false) // Mode = None Transfer Mode
                                        {
                                            Stopwatch stopwatch = new Stopwatch();
                                            stopwatch.Start();
                                            while (stopwatch.ElapsedMilliseconds <= numbers[counter].FinishCallTimer) { }
                                            try
                                            {
                                                numbers[counter].CallStates.Distinct();
                                                call.HangUp();
                                                call = null;
                                            }
                                            catch { }
                                            stopwatch.Stop();
                                            stopwatch.Reset();
                                        }
                                        else if (e.StatusCode == 200 && e.StatusCode != codeStop1 && e.StatusCode != codeStopAll && e.State == CallState.InCall && Operation == true)
                                        {
                                            CallStartEvent(call, e); // Mode = Attend , Blind For Transfer Mode Invoke Events
                                        }
                                    };
                                    call.Start();

                                    Stopwatch stopwatch1 = new Stopwatch();
                                    stopwatch1.Start();
                                    while (stopwatch1.ElapsedMilliseconds <= numbers[counter].CallTimer) { }
                                    try
                                    {
                                        call.HangUp();
                                        call = null;
                                    }
                                    catch { }
                                    stopwatch1.Stop();
                                    stopwatch1.Reset();

                                    Task.Delay(500).Wait();

                                    //Repaetetive mode 
                                    List<NumberModel> numberModelsNotTaken = numbers.Where(x => x.isTaken == false).ToList();
                                    switch (RepeatMode)
                                    {
                                        case "ByTimer":
                                            if (numberModelsNotTaken.Count >= Numbers.Count && TimeRepaet != 0 && RegistManyAccounts.StartStopState == true)
                                                foreach (NumberModel number in Numbers)
                                                    number.isTaken = false; TimeRepaet--;
                                            break;
                                        case "ForEver":
                                            if (numberModelsNotTaken.Count >= Numbers.Count && RegistManyAccounts.StartStopState == true)
                                                foreach (NumberModel number in Numbers)
                                                    number.isTaken = false;
                                            break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                var accountModel = RegistManyAccounts.AccountModels.Where(x => x.SIPAccount.RegisterName == phoneLine.SIPAccount.RegisterName).First();
                                RegistManyAccounts.RefreshAccount(phoneLine, accountModel);
                            }
                        }
                    );
                    Task.Delay(50).Wait();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RegistrationAccount(object sender, RegistrationStateChangedArgs e)
        {
            try
            {
                var data = (Tuple<IPhoneLine, ISoftPhone>)sender;
                Call(((IPhoneLine)data.Item1), ((ISoftPhone)data.Item2));  // when vpn start chek this line if work

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            //MessageBox.Show("registered");
        }


        protected virtual void CallStartEvent(object sender, CallStateChangedArgs e)
        {
            CallStart handler = callStartEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}
