using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.TransferCalls
{
    internal class stopBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public TransferCallsViewModel TransferCallsViewModel { get; set; }
        public stopBTN(TransferCallsViewModel transferCallsViewModel)
        {
            TransferCallsViewModel=transferCallsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            TransferCallsViewModel.TransferCalls.CallManyNumbers.RegistManyAccounts.StartStopState = false;
        }
    }
}
