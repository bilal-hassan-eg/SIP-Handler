using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIPManagementNewMVVM.Model.OzekiModels;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.Home
{
    internal class RegistBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public HomeViewModel HomeViewModel { get; set; }
        public RegistBTN(HomeViewModel homeViewModel)
        {
            HomeViewModel=homeViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            HomeViewModel.OzekiModels.Data = HomeViewModel.NumberModel;
            HomeViewModel.OzekiModels.SIPModel.Regist();
        }
    }
}
