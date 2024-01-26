using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.SIP
{
    internal class unRegistAllBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public SIPViewModel SIPViewModel { get; set; }
        public unRegistAllBTN(SIPViewModel sIPViewModel)
        {
            SIPViewModel=sIPViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SIPViewModel.RegistManyAccounts.UnRegistAll();
        }
    }
}
