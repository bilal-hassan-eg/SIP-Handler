﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.TransferCalls
{
    internal class insertNumberTextBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public TransferCallsViewModel TransferCallsViewModel { get; set; }
        public insertNumberTextBTN(TransferCallsViewModel transferCallsViewModel)
        {
            TransferCallsViewModel = transferCallsViewModel;
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
                TransferCallsViewModel._tempNumber = File.ReadAllText(openFileDialog.FileName);
            }
        }
    }
}
