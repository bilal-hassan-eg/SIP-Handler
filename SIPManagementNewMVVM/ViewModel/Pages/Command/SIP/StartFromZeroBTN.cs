using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIPManagementNewMVVM.ViewModel.Pages;
using SIPManagementNewMVVM.Model.OzekiModels;
using SIPManagementNewMVVM.Model.DataModel;
using System.Windows;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.SIP
{
    internal class StartFromZeroBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public SIPViewModel SIPViewModel { get; set; }
        public StartFromZeroBTN(SIPViewModel sIPViewModel)
        {
            SIPViewModel=sIPViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(SIPViewModel._AccountModels.Count > 0)
            {
                foreach(AccountModel account in SIPViewModel._AccountModels)
                {
                    account.isTaken = false;
                }
                SIPViewModel.RegistManyAccounts.StartStopState = true;
                SIPViewModel.RegistManyAccounts.PhoneLines?.Clear();

                SIPViewModel.RegistManyAccounts.Operation = false;
                foreach (AccountModel account in SIPViewModel._AccountModels)
                {
                    account.RegisterRequier = SIPViewModel.RegisterRequire;
                }
                SIPViewModel.RegistManyAccounts.AccountModels = SIPViewModel._AccountModels;
                SIPViewModel.RegistManyAccounts.ThreadNumber = SIPViewModel._ThreadNumber;
                SIPViewModel.RegistManyAccounts.RegistTimer = SIPViewModel._RegisterTime;
                SIPViewModel.RegistManyAccounts.TransportType = SIPViewModel._TransportType;

                SIPViewModel.RegistManyAccounts.Regist();
            }
            else
            {
                MessageBox.Show("Enter Data From File First");
            }
        }
    }
}
