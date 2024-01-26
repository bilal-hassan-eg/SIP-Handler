using Ozeki.VoIP;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPManagementNewMVVM.Model.DataModel;
using SIPManagementNewMVVM.ViewModel.Pages;
using Ozeki.Media;
using System.Diagnostics;
namespace SIPManagementNewMVVM.Model.OzekiModels
{
    internal class CallOneNumber 
    {
        public NumberModel Data { get; set; }       
        public RegistOneAccount SIPModel { get; set; }
        public IPhoneCall PhoneCall { get; set; }
        
        
        public Speaker speaker;
        public MediaConnector connector;
        public PhoneCallAudioReceiver mediaReceiver;


        List<int> CallStates = new List<int>();
        int CallState = 0;


        public CallOneNumber(NumberModel Data)
        {
            this.Data = Data;
            SIPModel = new RegistOneAccount(Data.AccountModel);
            speaker = Speaker.GetDefaultDevice();
            mediaReceiver = new PhoneCallAudioReceiver();
            connector = new MediaConnector();
        }

        public void Call(ref List<NumberModel> numberModels)
        {
            try
            {

            }
            catch { }
            List<NumberModel> numbers = numberModels;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    NumberModel CallObj = numbers.Find(x => x.PhoneNumber == Data.PhoneNumber);
                    SIPAccount accountModel = CallObj.AccountModel.SIPAccount;
                    PhoneCall = this.SIPModel.SoftPhone.CreateCallObject(SIPModel.PhoneLine, Data.PhoneNumber.Trim());
                    Data.CallID = PhoneCall.CallID;
                    PhoneCall.CallStateChanged += delegate (object sender, CallStateChangedArgs e)
                    {
                        // export accounts & call to txt file
                        string AccountLine = $"{accountModel.DomainServerHost}|{accountModel.DisplayName}|{accountModel.RegisterName}|{accountModel.UserName}|{accountModel.RegisterPassword}|{accountModel.OutboundProxy}|{CallObj.AccountModel.state}|{CallObj.PhoneNumber}|{e.StatusCode}:{e.Reason}\n";
                        File.AppendAllText($"./Home_Page/call_{CallObj.PhoneNumber}.txt", AccountLine);
                        CallObj._CallState = e.StatusCode;
                        CallObj.CallStates.Add(e.StatusCode);
                        CallObj.CallReasons.Add(e.State.ToString());
                        if (e.StatusCode == 200 && e.State == Ozeki.VoIP.CallState.InCall)
                        {
                            Stopwatch stopwatch = new Stopwatch();
                            stopwatch.Start();
                            while (stopwatch.ElapsedMilliseconds <= Data.FinishCallTimer) { }
                            try
                            {
                                CallObj.CallStates.Distinct();
                                PhoneCall.HangUp();
                                PhoneCall = null;
                            }
                            catch { }
                            stopwatch.Stop();
                            stopwatch.Reset();
                        }
                    };
                    PhoneCall.Start();

                    Stopwatch stopwatch1 = new Stopwatch();
                    stopwatch1.Start();
                    while (stopwatch1.ElapsedMilliseconds <= Data.CallTimer) { }
                    try
                    {
                        PhoneCall.HangUp();
                        PhoneCall = null;
                    }
                    catch { }
                    stopwatch1.Stop();
                    stopwatch1.Reset();
                }
                catch {
                    SIPModel.Regist(); //just for refreshing account after some time connection interrupt this line for refresh account in each exception have fun BRO :D 
                }              
            });

        }

        public List<int> getCallStates()
        {
            return CallStates;
        }
        public int getCallState()
        {
            return CallState;
        }

        public void HangUP()
        {
            try
            {
                PhoneCall.HangUp();
            }
            catch { }
        }

        public void OpenSpeaker()
        {
            connector.Connect(mediaReceiver, speaker);
            mediaReceiver.AttachToCall(PhoneCall);
            speaker.Start();
        }
    }
}
