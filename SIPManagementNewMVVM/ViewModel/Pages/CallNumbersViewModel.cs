using Ozeki.VoIP;
using SIPManagementNewMVVM.Model.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SIPManagementNewMVVM.ViewModel.Pages.Command.CallNumbers;
using SIPManagementNewMVVM.Model.OzekiModels;
using System.IO;

namespace SIPManagementNewMVVM.ViewModel.Pages
{
    internal class CallNumbersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CallManyNumbers CallManyNumbers { get; set; }

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

        public List<NumberModel> NumberModels { get; set; }
        public List<NumberModel> _NumberModels
        {
            get { return NumberModels; }
            set
            {
                if (value == null)
                    return;
                NumberModels = value;
                RaisePropertyChangedEvent();
            }
        }

        string userAgent { get; set; }
        public string _userAgent
        {
            get { return userAgent; }
            set
            {
                if (value == null)
                    return;
                userAgent = value;
                RaisePropertyChangedEvent();
            }
        }

        int registTimer { get; set; } = 1000;
        public int _registTimer { get { return registTimer; }
            set
            {
                if (value == null)
                    return;
                registTimer = value;
                RaisePropertyChangedEvent();
            }
        }

        int ringTimer { get; set; } = 1000;
        public int _ringTimer
        {
            get { return ringTimer; }
            set
            {
                if (value == null)
                    return;
                ringTimer = value;
                RaisePropertyChangedEvent();
            }
        }

        string transportType { get; set; }
        public string _transportType
        {
            get { return transportType; }
            set
            {
                if (value == null)
                    return;
                transportType = value;
                RaisePropertyChangedEvent();
            }
        }

        bool registerRequire { get; set; } = true;
        public bool _registerRequire
        {
            get { return registerRequire; }
            set
            {
                if (value == null)
                    return;
                registerRequire = value;
                RaisePropertyChangedEvent();
            }
        }

        string errorCode { get; set; } = "999";
        public string _errorCode
        {
            get { return errorCode; }
            set
            {
                if (value == null)
                    return;
                errorCode = value;
                RaisePropertyChangedEvent();
            }
        } 
        string errorHandelr { get; set; } 
        public string _errorHandelr
        {
            get { return errorHandelr; }
            set
            {
                if (value == null)
                    return;
                errorHandelr = value;
                RaisePropertyChangedEvent();
            }
        }

        int threadAccount { get; set; } = 2;
        public int _threadAccount
        {
            get { return threadAccount; }
            set
            {
                if (value == null)
                    return;
                threadAccount = value;
                RaisePropertyChangedEvent();
            }
        }

        int threadNumber { get; set; } = 2;
        public int _threadNumber
        {
            get { return threadNumber; }
            set
            {
                if (value == null)
                    return;
                threadNumber = value;
                RaisePropertyChangedEvent();
            }
        }

        string tempNumber { get; set; }
        public string _tempNumber
        {
            get { return tempNumber; }
            set
            {
                if (value == null)
                    return;
                tempNumber = value;
                RaisePropertyChangedEvent();
            }
        }

        public insertAccountTextBTN InsertAccountTextBTN { get; set; }
        public insertNumberTextBTN InsertNumberTextBTN { get; set; }
        public pauseBTN PauseBTN { get; set; }
        public startBTN StartBTN { get; set; }
        public startFromZeroBTN StartFromZeroBTN { get; set; }

        public CallNumbersViewModel()
        {
            if (!File.Exists("./user_agent.txt"))
                File.Create("./user_agent.txt").Close();

            _userAgent = File.ReadAllText("./user_agent.txt");

            this.InsertAccountTextBTN = new insertAccountTextBTN(this);
            this.InsertNumberTextBTN = new insertNumberTextBTN(this);
            this.PauseBTN = new pauseBTN(this);
            this.StartBTN = new startBTN(this);
            this.StartFromZeroBTN = new startFromZeroBTN(this);
            this.CallManyNumbers = new CallManyNumbers(_userAgent);
        }

        public void RaisePropertyChangedEvent([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
