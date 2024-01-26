using SIPManagementNewMVVM.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.TransferCalls
{
    internal class startBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public TransferCallsViewModel TransferCallsViewModel { get; set; }
        public startBTN(TransferCallsViewModel transferCallsViewModel)
        {
            TransferCallsViewModel=transferCallsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            List<AccountModel> accountModels = new List<AccountModel>()
            {
                TransferCallsViewModel._AccountModel
            };
            var transfer = TransferCallsViewModel.TransferCalls;
            transfer.CallManyNumbers.codeStop1 = TransferCallsViewModel._CodeStop1;
            transfer.CallManyNumbers.codeStopAll = TransferCallsViewModel._CodeStopAll;
            transfer.CallManyNumbers.RepeatMode = TransferCallsViewModel.RepeatModes.Where(x => x.isChecked == true).First().Content;
            transfer.CallManyNumbers.TimeRepaet = TransferCallsViewModel._repeatTime;
            transfer.userAgent = TransferCallsViewModel._AccountModel.UserAgent;
            transfer.Mode = TransferCallsViewModel.TranferModes.Where(x => x.isChecked == true).First().Content;

            List<string> numbers = TransferCallsViewModel._tempNumber.Split('\n').ToList();
            
            foreach(string item in numbers)
            {
                TransferCallsViewModel._Numbers.Add(new NumberModel(TransferCallsViewModel._AccountModel, TransferCallsViewModel._ringTimer, TransferCallsViewModel._callTimer, item));
            }

            transfer.CallManyNumbers.Numbers = TransferCallsViewModel._Numbers;
            transfer.CallManyNumbers.RegistManyAccounts.AccountModels =  accountModels;
            transfer.CallManyNumbers.RegistManyAccounts.ThreadNumber = 1;
            transfer.CallManyNumbers.RegistManyAccounts.isTransfer = true;
            transfer.CallManyNumbers.RegistManyAccounts.Operation = true;
            transfer.CallManyNumbers.RegistManyAccounts.StartStopState = true;
            transfer.CallManyNumbers.ThreadNumber = TransferCallsViewModel._threadNumber;
            transfer.CallManyNumbers.RegistManyAccounts.Regist();
        }
    }
}
