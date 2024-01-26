using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ozeki.VoIP;
using Ozeki.Network;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIPManagementNewMVVM.Model.DataModel
{
    internal class AccountModel : INotifyPropertyChanged
    {
        public int RegistTime { get; set; } = 1000;
        public int RefreshTime { get; set; } = 1000;
        public string UserAgent { get; set; }
        public string TransportType { get; set; }
        public SIPAccount SIPAccount { get; set; } = new SIPAccount();
        public bool RegisterRequier { get; set; } = true;
        public AccountModel(int registTime, int refreshTime, string userAgent, bool registerRequire, string transportType, SIPAccount sIPAccount)
        {
            RegistTime=registTime;
            RefreshTime=refreshTime;
            UserAgent=userAgent;
            TransportType=transportType;
            SIPAccount = sIPAccount;
            SIPAccount.RegistrationRequired = registerRequire;
        }
        public AccountModel(SIPAccount SipAccount)
        {
            this.SIPAccount = SipAccount;
        }
        public AccountModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public int state { get; set; }
        public int _state
        {
            get { return state; }
            set
            {
                if (value == null)
                    return;
                state = value;
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
