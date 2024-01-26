using SIPManagementNewMVVM.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPManagementNewMVVM.Model.ComponentModel;
using SIPManagementNewMVVM.Model.DataModel;
using System.ComponentModel;
using SIPManagementNewMVVM.View.Windows;
using System.Runtime.CompilerServices;
using SIPManagementNewMVVM.ViewModel.Windows.Command.MainWindow;
namespace SIPManagementNewMVVM.ViewModel.Windows
{
    internal class mainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow MainWindow { get; set; }

        public List<MainWindowItemList> MainWindowItemList { get; set; }
        public List<MainWindowItemList> _MainWindowItemList
        {
            get { return MainWindowItemList; }
            set
            {
                if (value == null)
                    return;
                MainWindowItemList = value;
                RaisePropertyChangedEvent();
            }
        }

        public mainWindowViewModel()
        {
            _MainWindowItemList = new List<MainWindowItemList>()
            {
                new MainWindowItemList("Home",new NavigationBTNS(this)),
                new MainWindowItemList("SIP",new NavigationBTNS(this)),
                new MainWindowItemList("Call Numbers",new NavigationBTNS(this)),
                new MainWindowItemList("Transfer Calls",new NavigationBTNS(this)),
                //new MainWindowItemList("User-Agent",new NavigationBTNS(this)),
            };
        }

        protected void RaisePropertyChangedEvent([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        internal void setContext(MainWindow mainWindow)
        {
            this.MainWindow = mainWindow;
        }
    }
}
