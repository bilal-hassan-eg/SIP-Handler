using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.TransferCalls
{
    internal class openSP : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public TransferCallsViewModel TransferCallsViewModel { get; set; }
        public openSP(TransferCallsViewModel transferCallsViewModel)
        {
            TransferCallsViewModel=transferCallsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            TransferCallsViewModel.TransferCalls.OpenSpeaker(TransferCallsViewModel._SelectedNumberModel.CallID);   
        }
    }
}
