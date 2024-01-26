using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIPManagementNewMVVM.View.Pages;

namespace SIPManagementNewMVVM.ViewModel.Windows.Command.MainWindow
{
    internal class NavigationBTNS : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public mainWindowViewModel MainWindowViewModel { get; set; }
        public NavigationBTNS(mainWindowViewModel mainWindowViewModel)
        {
            MainWindowViewModel=mainWindowViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "SIP":
                    SIPPage SIP = new SIPPage();
                    MainWindowViewModel.MainWindow.Frame.NavigationService.Navigate(SIP);
                    break;
                case "Home":
                    HomePage Home = new HomePage();
                    MainWindowViewModel.MainWindow.Frame.NavigationService.Navigate(Home);
                    break;
                case "Call Numbers":
                    CallNumbersPage callNumbers = new CallNumbersPage();
                    MainWindowViewModel.MainWindow.Frame.NavigationService.Navigate(callNumbers);
                    break;
                case "Transfer Calls":
                    TransferCallPage transferCall = new TransferCallPage();
                    MainWindowViewModel.MainWindow.Frame.NavigationService.Navigate(transferCall);
                    break;
                case "User-Agent":
                    userAgentPage userAgent = new userAgentPage();
                    MainWindowViewModel.MainWindow.Frame.NavigationService.Navigate(userAgent);
                    break;
            }
        }
    }
}
