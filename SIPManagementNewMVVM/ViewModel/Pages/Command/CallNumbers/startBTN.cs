using SIPManagementNewMVVM.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.CallNumbers
{
    internal class startBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;
        
        public CallNumbersViewModel CallNumbersViewModel { get; set; }
        public startBTN(CallNumbersViewModel callNumbersViewModel) 
        { 
            this.CallNumbersViewModel = callNumbersViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (CallNumbersViewModel._AccountModels.Count > 0)
            {
                var register = CallNumbersViewModel.CallManyNumbers.RegistManyAccounts;
                foreach(AccountModel account in CallNumbersViewModel._AccountModels)
                {
                    account.RegisterRequier = CallNumbersViewModel._registerRequire;
                }
                register.AccountModels = CallNumbersViewModel._AccountModels;
                register.userAgent = CallNumbersViewModel._userAgent;
                register.ThreadNumber = CallNumbersViewModel._threadNumber;
                register.RegistTimer = CallNumbersViewModel._registTimer;
                register.TransportType = CallNumbersViewModel._transportType;
                register.StartStopState = true;
                register.Operation = true;
                register.isTransfer = false;
                register.errorHandler = CallNumbersViewModel._errorHandelr;
                register.errorCode = CallNumbersViewModel._errorCode;
                if (CallNumbersViewModel._errorHandelr == "Continue")
                    CallNumbersViewModel.CallManyNumbers.RepeatMode = "ForEver";
                CallNumbersViewModel.CallManyNumbers.ThreadNumber = CallNumbersViewModel._threadNumber;
                CallNumbersViewModel.CallManyNumbers.userAgent = CallNumbersViewModel._userAgent;
                


                CallNumbersViewModel.CallManyNumbers.RegistManyAccounts.Regist();
            }
            else
            {
                MessageBox.Show("Enter Data From File First");
            }
        }
    }
}
