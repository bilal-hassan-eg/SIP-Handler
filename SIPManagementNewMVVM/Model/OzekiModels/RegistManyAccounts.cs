using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ozeki.VoIP;
using SIPManagementNewMVVM.Model.DataModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Input;
using onvif.services;
using SIPManagementNewMVVM.Model.DataHandel;
using System.Windows;

namespace SIPManagementNewMVVM.Model.OzekiModels
{
    internal class RegistManyAccounts
    {
        public List<ISoftPhone> SoftPhones { get; set; }
        public List<IPhoneLine> PhoneLines { get; set; }

        public List<AccountModel> AccountModels { get; set; }
        public string userAgent { get; set; }
        public int ThreadNumber { get; set; }
        public string TransportType { get; set; }
        public int RegistTimer { get; set; }
        public bool StartStopState { get; set; } = true; // true for strat false for stop
        public string errorHandler { get; set; } = "";
        public string errorCode { get; set; } = "999";
        public bool Operation { get; set; } // false for regist and un regist true for regist only
        public bool isTransfer { get; set; } // for export options

        public delegate void RegistirationChange(object sender, RegistrationStateChangedArgs e);
        public event RegistirationChange registEvent;

        public RegistManyAccounts(List<AccountModel> accountModels, int threadNumber, string transportType, int registTimer, bool registerRequire , string userAgent, bool operation)
        {
            this.AccountModels = accountModels;
            PhoneLines = new List<IPhoneLine>();
            SoftPhones = new List<ISoftPhone>();
            ThreadNumber=threadNumber;
            TransportType=transportType;
            RegistTimer=registTimer;
            this.Operation = operation;
            this.userAgent = userAgent;
        }

        public RegistManyAccounts(string userAgent)
        {
            SoftPhones = new List<ISoftPhone>();
            PhoneLines = new List<IPhoneLine>();
            this.userAgent = userAgent; 
        }


        public void Regist()
        {
            Export.OBJ = new object();
            Export.OBJ1 = new object();
            SoftPhones = new List<ISoftPhone>();
            PhoneLines = new List<IPhoneLine>();
            for (int i = 0; i < ThreadNumber; i++)
            {
                Task.Run(
                    delegate ()
                    {
                        try
                        {
                            for (int counter = 0; counter < AccountModels.Count - 1; counter++)
                            {

                                Stopwatch stopwatch = new Stopwatch();
                                if (!StartStopState)
                                {
                                    //MessageBox.Show("BREAKED");
                                    break;
                                }
                                if (AccountModels[counter].isTaken)
                                {
                                    //MessageBox.Show("Continued");
                                    continue;
                                }
                                AccountModels[counter].isTaken = true;
                                //MessageBox.Show($"Thread : {ThreadNumber} , Accounts Number : {AccountModels.Count} , Account regist : {AccountModels[counter].SIPAccount.RegisterName}");
                                ISoftPhone SoftPhone = SoftPhoneFactory.CreateSoftPhone(5000, 10000, userAgent);
                                SIPAccount accountModel = AccountModels[counter].SIPAccount;
                                accountModel.RegistrationRequired = AccountModels[counter].RegisterRequier;
                                PhoneLineConfiguration configuration = new PhoneLineConfiguration(accountModel);
                                switch (TransportType)
                                {
                                    case "TCP":
                                        configuration.TransportType = Ozeki.Network.TransportType.Tcp;
                                        break;
                                    case "UDP":
                                        configuration.TransportType = Ozeki.Network.TransportType.Udp;
                                        break;
                                }
                                IPhoneLine PhoneLine = SoftPhone.CreatePhoneLine(configuration);
                                PhoneLines.Add(PhoneLine);
                                SoftPhones.Add(SoftPhone);
                                PhoneLine.RegistrationStateChanged += delegate (object sender, RegistrationStateChangedArgs e)
                                {
                                    // export accounts to txt file
                                    AccountModels[counter]._state = e.StatusCode;
                                    AccountModels[counter]._reason = e.ReasonPhrase;


                                    switch (Operation) // Export Options
                                    {
                                        case true:
                                            if (isTransfer == false)
                                                //logAccount.Add(new Tuple<SIPAccount, string, int>(accountModel, "list_call", e.StatusCode));
                                                Export.ExportAccount(accountModel, "list_call", e.StatusCode);
                                            else
                                                //logAccount.Add(new Tuple<SIPAccount, string, int>(accountModel, "transfer_calls", e.StatusCode));
                                                Export.ExportAccount(accountModel, "transfer_calls", e.StatusCode);
                                            break;
                                        case false:
                                            //logAccount.Add(new Tuple<SIPAccount, string, int>(accountModel, "list_sip", e.StatusCode));
                                            Export.ExportAccount(accountModel, "list_sip", e.StatusCode);
                                            break;
                                    }

                                    if (e.StatusCode == 200 && Operation == false)
                                    {
                                        UnRegistOne(PhoneLine , SoftPhone);
                                    }
                                    else if(e.StatusCode == Int32.Parse(errorCode) && Operation && errorHandler == "Stop Without Accounts Errors")
                                    {
                                        StartStopState = false;
                                        MessageBox.Show($"Error Account Reg : {accountModel.RegisterName}");
                                    }
                                    else if (e.StatusCode == 200 && Operation == true)
                                    {
                                        var tuple = new Tuple<IPhoneLine, ISoftPhone>(PhoneLine, SoftPhone);
                                        RegistrationEvent(tuple, e); // invoke events
                                    }
                                };
                                SoftPhone.RegisterPhoneLine(PhoneLine);
                                if (RegistTimer != 0 && Operation == false) 
                                {
                                    stopwatch.Start();
                                    while (stopwatch.ElapsedMilliseconds <= RegistTimer) 
                                    {
                                        if (PhoneLine.RegistrationInfo.IsRegistered)
                                            break;
                                    }
                                    stopwatch.Stop();
                                    UnRegistOne(PhoneLine, SoftPhone);
                                }
                                Task.Delay(500).Wait();
                            }
                        }
                        catch(Exception ex) 
                        {
                            MessageBox.Show("REG : "  +  ex.Message);
                        }     
                    }
                );
                Task.Delay(100).Wait(); // just for make diffrence between thread to get account for all thread first account
            }

        }




        // THIS METHODS FOR OPERATION ON ONE ACCOUNT UNREGIST ONE UNRIGST ALL REFRESH

        public void UnRegistOne(IPhoneLine phoneLine , ISoftPhone softPhone)
        {
            try
            {
                softPhone.UnregisterPhoneLine(phoneLine);
                softPhone.Close();
                PhoneLines.Remove(phoneLine);
                SoftPhones.Remove(softPhone);
            }
            catch(Exception ex) {
                MessageBox.Show("UN : " +  ex.Message + $" TIME : PH {PhoneLines.Count}   SH : {SoftPhones.Count} ");
            }
        }
        
        public void UnRegistAll()
        {
            try
            {
                for(int i = 0; i < PhoneLines.Count; i++)
                {
                    SoftPhones[i].UnregisterPhoneLine(PhoneLines[i]);
                }
            }
            catch { }
        }
        public void RefreshAccount(IPhoneLine phoneLine , AccountModel account)
        {
            Task.Run(
                delegate ()
                {
                    Stopwatch stopwatch = new Stopwatch();
                    SIPAccount accountModel = account.SIPAccount;
                    phoneLine.RegistrationStateChanged += delegate (object sender, RegistrationStateChangedArgs e) {
                        string AccountLine = $"{accountModel.DomainServerHost}|{accountModel.DisplayName}|{accountModel.RegisterName}|{accountModel.UserName}|{accountModel.RegisterPassword}|{accountModel.OutboundProxy}|{e.StatusCode}\n";
                        account._state = e.StatusCode;
                        account._reason = e.ReasonPhrase;
                        File.AppendAllText($"./list_call/account_{e.StatusCode}_refresh.txt", AccountLine);
                        RegistrationEvent(phoneLine, e); // after refreshing account will invoke event again but will start call from the last number and will create threads second turn
                    };
                    ISoftPhone softPhone = SoftPhones[PhoneLines.IndexOf(phoneLine)];
                    softPhone.RegisterPhoneLine(phoneLine);
                }
            ).Wait();
        }


        // for send phoneline which is successfully registerd to call class to call number on it

        protected virtual void RegistrationEvent(object sender,RegistrationStateChangedArgs e)
        {
            RegistirationChange handler = registEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}
