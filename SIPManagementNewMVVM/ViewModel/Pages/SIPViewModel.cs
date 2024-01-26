using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SIPManagementNewMVVM.Model.DataModel;
using SIPManagementNewMVVM.ViewModel.Pages.Command.SIP;
using SIPManagementNewMVVM.Model.OzekiModels;
namespace SIPManagementNewMVVM.ViewModel.Pages
{
    internal class SIPViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<AccountModel> AccountModels { get; set; }
        public List<AccountModel> _AccountModels
        {
            get { return AccountModels; }
            set
            {
                if (value == null)
                    return;
                AccountModels = value;
                RaisePropertyChangedEvent();
            }
        }

        public AccountModel SelectedAccount { get; set; }
        public AccountModel _SelectedAccount
        {
            get { return SelectedAccount; }
            set
            {
                if (value == null)
                    return;
                SelectedAccount = value;
                RaisePropertyChangedEvent();
            }
        }

        public int ThreadNumber { get; set; } = 2;
        public int _ThreadNumber
        {
            get { return ThreadNumber; }
            set
            {
                if (value == null)
                    return;
                ThreadNumber = value;
                RaisePropertyChangedEvent();
            }
        }

        public int RegisterTime { get; set; } = 1000;
        public int _RegisterTime
        {
            get { return RegisterTime; }
            set
            {
                if (value == null)
                    return;
                RegisterTime = value;
                RaisePropertyChangedEvent();
            }
        }

        public bool RegisterRequire { get; set; } = true;
        public bool _RegisterRequire
        {
            get { return RegisterRequire; }
            set
            {
                if (value == null)
                    return;
                RegisterRequire = value;
                RaisePropertyChangedEvent();
            }
        }

        public string UserAgent { get; set; }
        public string _UserAgent
        {
            get { return UserAgent; }
            set
            {
                if (value == null)
                    return;
                UserAgent = value;
                RaisePropertyChangedEvent();
            }
        }

        public string TransportType { get; set; }
        public string _TransportType
        {
            get { return TransportType; }
            set
            {
                if (value == null)
                    return;
                TransportType = value;
                RaisePropertyChangedEvent();
            }
        }

        public insertTextBTN InsertTextBTN { get; set; }
        public StartFromZeroBTN StartFromZeroBTN { get; set; }
        public startRegistBTN StartRegistBTN { get; set; }
        public pauseRegistBTN PauseRegistBTN { get; set; }
        public unRegistAllBTN UnRegistAllBTN { get; set; }
        public UnRegistOneBTN UnRegistOneBTN { get; set; }

        public RegistManyAccounts RegistManyAccounts { get; set; }

        public SIPViewModel()
        {
            StartRegistBTN = new startRegistBTN(this);
            PauseRegistBTN = new pauseRegistBTN(this);
            StartFromZeroBTN = new StartFromZeroBTN(this);
            InsertTextBTN = new insertTextBTN(this);
            AccountModels = new List<AccountModel>();

            if (!File.Exists("./user_agent.txt"))
                File.Create("./user_agent.txt").Close();

            _UserAgent = File.ReadAllText("./user_agent.txt");

            RegistManyAccounts = new RegistManyAccounts(_UserAgent);

            if (!Directory.Exists("./SIP"))
                Directory.CreateDirectory("./SIP");
        }

        protected void RaisePropertyChangedEvent([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
