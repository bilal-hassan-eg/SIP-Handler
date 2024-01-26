using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.CallNumbers
{
    internal class insertNumberTextBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public CallNumbersViewModel CallNumbersViewModel { get; set; }
        public insertNumberTextBTN(CallNumbersViewModel callNumbersViewModel)
        {
            CallNumbersViewModel=callNumbersViewModel;
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
                CallNumbersViewModel._tempNumber = File.ReadAllText(openFileDialog.FileName);
            }
        }
    }
}
