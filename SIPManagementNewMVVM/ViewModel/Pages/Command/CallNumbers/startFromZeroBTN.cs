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
    internal class startFromZeroBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public CallNumbersViewModel CallNumbersViewModel { get; set; }
        public startFromZeroBTN(CallNumbersViewModel callNumbersViewModel)
        {
            CallNumbersViewModel=callNumbersViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                if (CallNumbersViewModel._AccountModels.Count > 0 && CallNumbersViewModel._tempNumber != "")
                {
                    var register = CallNumbersViewModel.CallManyNumbers.RegistManyAccounts;
                    foreach (AccountModel account in CallNumbersViewModel._AccountModels)
                    {
                        account.RegisterRequier = CallNumbersViewModel._registerRequire;
                        account._state = 0;
                        account._reason = "";
                    }
                    register.AccountModels = CallNumbersViewModel._AccountModels;
                    register.userAgent = CallNumbersViewModel._userAgent;
                    register.ThreadNumber = CallNumbersViewModel._threadNumber;
                    register.RegistTimer = CallNumbersViewModel._registTimer;
                    register.TransportType = CallNumbersViewModel._transportType;
                    register.StartStopState = true;
                    register.Operation = true;
                    register.errorHandler = CallNumbersViewModel._errorHandelr;
                    register.errorCode = CallNumbersViewModel._errorCode;
                    if(CallNumbersViewModel._errorHandelr == "Continue")
                        CallNumbersViewModel.CallManyNumbers.RepeatMode = "ForEver";
                    CallNumbersViewModel.CallManyNumbers.ThreadNumber = CallNumbersViewModel._threadNumber;
                    CallNumbersViewModel.CallManyNumbers.userAgent = CallNumbersViewModel._userAgent;

                    List<string> Numbers = CallNumbersViewModel._tempNumber.Split('\n').ToList();
                    List<NumberModel> num = new List<NumberModel>();
                    foreach (string number in Numbers)
                    {
                        foreach (AccountModel obj in CallNumbersViewModel.AccountModels)
                        {
                            if(number.Trim().Length  > 0)
                                num.Add(new Model.DataModel.NumberModel(obj, CallNumbersViewModel._ringTimer, 0, number));
                        }

                    }
                    CallNumbersViewModel._NumberModels = num;
                    foreach (AccountModel account in CallNumbersViewModel._AccountModels)
                    {
                        account.isTaken = false;
                    }

                    CallNumbersViewModel.CallManyNumbers.Numbers = CallNumbersViewModel._NumberModels;

                    CallNumbersViewModel.CallManyNumbers.RegistManyAccounts.Regist();
                }
                else
                {
                    MessageBox.Show("Enter Data From File First");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
