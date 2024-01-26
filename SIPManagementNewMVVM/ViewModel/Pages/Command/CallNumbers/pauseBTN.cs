using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.CallNumbers
{
    internal class pauseBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public CallNumbersViewModel CallNumbersViewModel { get; set; }
        public pauseBTN(CallNumbersViewModel callNumbersViewModel)
        {
            CallNumbersViewModel=callNumbersViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            CallNumbersViewModel.CallManyNumbers.RegistManyAccounts.StartStopState = false;
        }
    }
}
