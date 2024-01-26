using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIPManagementNewMVVM.ViewModel.Pages;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.SIP
{
    internal class pauseRegistBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;
        SIPViewModel SIPViewModel { get; set; }
        public pauseRegistBTN(SIPViewModel sIPViewModel)
        {
            SIPViewModel=sIPViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SIPViewModel.RegistManyAccounts.StartStopState = false;
        }
    }
}
