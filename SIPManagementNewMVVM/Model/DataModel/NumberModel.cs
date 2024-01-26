using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ozeki.VoIP;
namespace SIPManagementNewMVVM.Model.DataModel
{
    internal class NumberModel : INotifyPropertyChanged
    {
        public AccountModel AccountModel { get; set; } = new AccountModel();
        public int CallTimer { get; set; } = 1000;
        public List<int> CallStates { get; set; } = new List<int>();
        public List<string> CallReasons { get; set; } = new List<string>();
        public int FinishCallTimer { get; set; } = 1000;
        public string PhoneNumber { get; set; } = "Phone Number";
        public string CallID { get; set; }


        public NumberModel(AccountModel accountModel, int callTimer, int finishCallTimer, string phoneNumber)
        {
            AccountModel=accountModel;
            CallTimer=callTimer;
            FinishCallTimer=finishCallTimer;
            PhoneNumber=phoneNumber;
        }
        public NumberModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public int CallState { get; set; }
        public int _CallState { get { return CallState; }
            set {
                if (value == null)
                    return;
                CallState = value;
                RaisePropertyChangedEvent();
            }
        }

        public string reason { get; set; }
        public string _reason
        {
            get { return reason; }
            set
            {
                if (value == null)
                    return;
                reason = value;
                RaisePropertyChangedEvent();
            }
        }
        public void RaisePropertyChangedEvent([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool isTaken { get; set; } = false;

    }
}
