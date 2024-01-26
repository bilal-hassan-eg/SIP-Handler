using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPManagementNewMVVM.Model.DataModel;
using System.Windows.Input;
using System.IO;
using System.Windows;
using SIPManagementNewMVVM.Model.DataHandel;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.SIP
{
    internal class insertTextBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public SIPViewModel SIPViewModel { get; set; }
        public insertTextBTN(SIPViewModel sIPViewModel)
        {
            SIPViewModel=sIPViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                SIPViewModel._AccountModels = new AccountTextFile(openFileDialog.FileName).Handel();
            }
        }
    }
}
