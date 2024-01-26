using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIPManagementNewMVVM.Model.DataHandel;


namespace SIPManagementNewMVVM.ViewModel.Pages.Command.CallNumbers
{
    internal class insertAccountTextBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public CallNumbersViewModel CallNumbersViewModel { get; set; }

        public insertAccountTextBTN(CallNumbersViewModel callNumbersViewModel)
        {
            this.CallNumbersViewModel = callNumbersViewModel;
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
                CallNumbersViewModel._AccountModels = new AccountTextFile(openFileDialog.FileName).Handel();
            }
        }
    }
}
