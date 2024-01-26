using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ozeki.VoIP;
using SIPManagementNewMVVM.Model.DataModel;
using SIPManagementNewMVVM.Model.DataHandel;
using Ozeki.Media;

namespace SIPManagementNewMVVM.Model.OzekiModels
{
    internal class TransferCalls 
    {
        public CallManyNumbers CallManyNumbers { get; set; }
        public string userAgent { get; set; }

        public string Mode { get; set; }

        public Speaker speaker;
        public MediaConnector connector;
        public PhoneCallAudioReceiver mediaReceiver;

        public List<ICall> AcceptedCall { get; set; }

        public TransferCalls(string userAgent)
        {
            CallManyNumbers = new CallManyNumbers(userAgent);
            CallManyNumbers.callStartEvent += CallAccepted;
            AcceptedCall = new List<ICall>();
        }

        public void AttendTransfer()
        {
            Task.Run(() =>
            {
                if (AcceptedCall.Count >= 2)
                {
                    AcceptedCall[0].AttendedTransfer(AcceptedCall[1]);

                    Export.ExportAttend(AcceptedCall[0], AcceptedCall[1] , CallManyNumbers.RegistManyAccounts.AccountModels[0].SIPAccount);

                    AcceptedCall.RemoveAt(0);
                    AcceptedCall.RemoveAt(1);
                }
            });
        }
        public void BlindTransfer()
        {
            Task.Run(() =>
            {
                if (AcceptedCall.Count >= 1)
                {
                    AcceptedCall[0].BlindTransfer(CallManyNumbers.Numbers[0].PhoneNumber);

                    Export.ExportBlind(AcceptedCall[0], CallManyNumbers.Numbers[0].PhoneNumber, CallManyNumbers.RegistManyAccounts.AccountModels[0].SIPAccount);

                    AcceptedCall.RemoveAt(0);
                }
            });
        }

        public void HangUp(string callID)
        {
            try
            {
                AcceptedCall.Where(x => x.CallID == callID).First().HangUp();
            }
            catch { }
        }

        public void OpenSpeaker(string callID)
        {
            connector.Connect(mediaReceiver, speaker);
            mediaReceiver.AttachToCall(AcceptedCall.Where(x => x.CallID == callID).First());
            speaker.Start();
        }


        public void CallAccepted(object sender, CallStateChangedArgs e)
        {
            AcceptedCall.Add(((ICall)sender)); // Events Invoke If Call Accepted 
            switch (Mode)
            {
                case "Attend":
                    AttendTransfer();
                    break;
                case "Blind":
                    BlindTransfer();
                    break;
            }
        }

    }
}
