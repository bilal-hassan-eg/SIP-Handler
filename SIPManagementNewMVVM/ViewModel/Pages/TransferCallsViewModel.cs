using SIPManagementNewMVVM.Model.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SIPManagementNewMVVM.Model.ComponentModel;
using SIPManagementNewMVVM.Model.OzekiModels;
using SIPManagementNewMVVM.ViewModel.Pages.Command.TransferCalls;
using System.IO;

namespace SIPManagementNewMVVM.ViewModel.Pages
{
    internal class TransferCallsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // in transfer model we use one account regist one account and do many calls operation
        // you can insert that account in list and use regist many account to regist it and do our operation normally
        // i think the second soluiton is better than the first so i will do second tomorrow insha allah :D

        public TransferCalls TransferCalls { get; set; }

        List<NumberModel> Numbers { get; set; }
        public List<NumberModel> _Numbers
        {
            get { return Numbers; }
            set
            {
                if (value == null)
                    return;
                Numbers = value;
                RaisePropertyChangedEvent();
            }
        }

        AccountModel AccountModel { get; set; }
        public AccountModel _AccountModel
        {
            get { return AccountModel; }
            set
            {
                if (value == null)
                    return;
                AccountModel = value;
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

        int repeatTime { get; set; } = 1;
        public int _repeatTime
        {
            get { return repeatTime; }
            set
            {
                if (value == null)
                    return;
                repeatTime = value;
                RaisePropertyChangedEvent();
            }
        }

        public int CodeStop1 { get; set; }
        public int _CodeStop1
        {
            get { return CodeStop1; }
            set
            {
                if (value == null)
                    return;
                CodeStop1 = value;
                RaisePropertyChangedEvent();
            }
        }

        public int CodeStopAll { get; set; }
        public int _CodeStopAll
        {
            get { return CodeStopAll; }
            set
            {
                if (value == null)
                    return;
                CodeStopAll = value;
                RaisePropertyChangedEvent();
            }
        }

        public int ringTimer { get; set; } = 1000;
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

        public int callTimer { get; set; } = 1000;
        public int _callTimer
        {
            get { return callTimer; }
            set
            {
                if (value == null)
                    return;
                callTimer = value;
                RaisePropertyChangedEvent();
            }
        }

        public List<RadioBtTranfser> TranferModes { get; set; }
        public List<RadioBtTranfser> RepeatModes { get; set; }

        public NumberModel SelectedNumberModel {get; set; }
        public NumberModel _SelectedNumberModel
        {
            get { return SelectedNumberModel; }
            set
            {
                if (value == null)
                    return;
                SelectedNumberModel = value;
                RaisePropertyChangedEvent();
            }
        }

        public string tempNumber { get; set; }
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

        public startBTN StartBTN { get; set; }
        public hangUpBTN HangUpBTN { get; set; }
        public openSP OpenSP { get; set; }
        public stopBTN StopBTN { get; set; }
        public unRegistBTN UnRegistBTN { get; set; }
        public insertNumberTextBTN InsertNumberTextBTN { get; set; }

        public TransferCallsViewModel()
        {
            this.TranferModes = new List<RadioBtTranfser>()
            {
                new RadioBtTranfser("Attend" , false),
                new RadioBtTranfser("Blind" , false),
                new RadioBtTranfser("None" , true),
            };
            this.RepeatModes = new List<RadioBtTranfser>()
            {
                new RadioBtTranfser("ForEver" , false),
                new RadioBtTranfser("ByTimer" , true)
            };

            StartBTN = new startBTN(this);
            HangUpBTN = new hangUpBTN(this);
            OpenSP = new openSP(this);
            StopBTN = new stopBTN(this);
            UnRegistBTN = new unRegistBTN(this);
            InsertNumberTextBTN = new insertNumberTextBTN(this);

            if (!File.Exists("./user_agent.txt"))
                File.Create("./user_agent.txt").Close();

            _AccountModel = new AccountModel();
            if (File.Exists("./account.txt"))
            {
                string[] Arr = File.ReadAllText("./account.txt").Split('|');
                _AccountModel.SIPAccount.DisplayName = Arr[1];
                _AccountModel.SIPAccount.DomainServerHost = Arr[0];
                _AccountModel.SIPAccount.RegisterName = Arr[2];
                _AccountModel.SIPAccount.UserName = Arr[3];
                _AccountModel.SIPAccount.RegisterPassword = Arr[4];
                _AccountModel.SIPAccount.OutboundProxy = Arr[5];
            }
            else
                File.Create("./account.txt");
            _AccountModel.UserAgent = File.ReadAllText("./user_agent.txt");

            TransferCalls = new TransferCalls(_AccountModel.UserAgent);
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
