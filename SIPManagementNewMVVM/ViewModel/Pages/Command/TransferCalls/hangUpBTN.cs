using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.TransferCalls
{
    internal class hangUpBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public TransferCallsViewModel TransferCallsViewModel { get; set; }
        public hangUpBTN(TransferCallsViewModel transferCallsViewModel)
        {
            TransferCallsViewModel=transferCallsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            TransferCallsViewModel.TransferCalls.HangUp(TransferCallsViewModel._SelectedNumberModel.CallID);
        }
    }
}
