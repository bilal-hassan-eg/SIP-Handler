using System;
using System.IO;
using System.Windows.Input;
using SIPManagementNewMVVM.ViewModel.Pages;
using SIPManagementNewMVVM.Model.DataModel;
using Ozeki.VoIP;
namespace SIPManagementNewMVVM.ViewModel.Pages.Command.Home
{
    internal class ExportBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;

        HomeViewModel HomeViewModel { get; set; }
        public ExportBTN(HomeViewModel homeViewModel)
        {
            HomeViewModel=homeViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            NumberModel numberModel = HomeViewModel._NumberModels[0];
            SIPAccount accountModel = numberModel.AccountModel.SIPAccount;
            for(int i = 0; i < numberModel.CallReasons.Count; i++)
            {
                string AccountLine = $"{accountModel.DomainServerHost}|{accountModel.DisplayName}|{accountModel.RegisterName}|{accountModel.UserName}|{accountModel.RegisterPassword}|{accountModel.OutboundProxy}|{numberModel.AccountModel.state}|{numberModel.PhoneNumber}|{numberModel.CallStates[i]}:{numberModel.CallReasons[i]}\n";
                File.AppendAllText($"./home_page.txt",AccountLine);
            }
        }
    }
}
