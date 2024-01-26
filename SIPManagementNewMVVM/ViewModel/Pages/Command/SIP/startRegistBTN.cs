using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIPManagementNewMVVM.ViewModel.Pages;
using SIPManagementNewMVVM.Model.OzekiModels;
using System.Windows;
using SIPManagementNewMVVM.Model.DataModel;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.SIP
{
    internal class startRegistBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public SIPViewModel SIPViewModel { get; set; }
        public startRegistBTN(SIPViewModel sIPViewModel)
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
                SIPViewModel.RegistManyAccounts.Operation = false;
                SIPViewModel.RegistManyAccounts.StartStopState = true;
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
