using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SIPManagementNewMVVM.Model.DataModel;
using SIPManagementNewMVVM.Model.OzekiModels;
using SIPManagementNewMVVM.ViewModel.Pages.Command.Home;
namespace SIPManagementNewMVVM.ViewModel.Pages
{
    internal class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public CallOneNumber OzekiModels ;


        public CallBTN CallBTN { get; set; }
        public ExportBTN ExportBTN { get; set; }
        public HangUpBTN HangUpBTN { get; set; }
        public RegistBTN RegistBTN { get; set; }
        public UNRegistBTN UNRegistBTN { get; set; }
        public SpeakerBTN SpeakerBTN { get; set; }
        
        public NumberModel NumberModel {get; set;}
        public NumberModel _NunberModel
        {
            get { return NumberModel; }
            set
            {
                if (value == null)
                    return;
                NumberModel = value;
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
        
        public NumberModel SelectedNumberModel { get; set; }
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
        

        public HomeViewModel()
        {
            _NumberModels = new List<NumberModel>();
            _NunberModel = new NumberModel();
            UNRegistBTN = new UNRegistBTN(this);
            RegistBTN = new RegistBTN(this);
            SpeakerBTN = new SpeakerBTN(this);
            HangUpBTN = new HangUpBTN(this);
            ExportBTN = new ExportBTN(this);
            CallBTN = new CallBTN(this);
            if (!File.Exists("./user_agent.txt"))
                File.Create("./user_agent.txt").Close();
            
            NumberModel.AccountModel.UserAgent = File.ReadAllText("./user_agent.txt");
            if (File.Exists("./account.txt"))
            {
                string[] Arr = File.ReadAllText("./account.txt").Split('|');
                NumberModel.AccountModel.SIPAccount.DisplayName = Arr[1];
                NumberModel.AccountModel.SIPAccount.DomainServerHost = Arr[0];
                NumberModel.AccountModel.SIPAccount.RegisterName = Arr[2];
                NumberModel.AccountModel.SIPAccount.UserName = Arr[3];
                NumberModel.AccountModel.SIPAccount.RegisterPassword = Arr[4];
                NumberModel.AccountModel.SIPAccount.OutboundProxy = Arr[5];
            }
            else
                File.Create("./account.txt");

            OzekiModels = new CallOneNumber(NumberModel);

            if (!Directory.Exists("./Home_Page"))
                Directory.CreateDirectory("./Home_Page");
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
