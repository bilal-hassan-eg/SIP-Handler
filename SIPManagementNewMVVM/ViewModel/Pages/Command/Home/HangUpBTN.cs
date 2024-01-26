using SIPManagementNewMVVM.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.Home
{
    internal class HangUpBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public HomeViewModel HomeViewModel { get; set; }
        public HangUpBTN(HomeViewModel homeViewModel)
        {
            HomeViewModel=homeViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            HomeViewModel._NumberModels = new List<NumberModel>();
            HomeViewModel.OzekiModels.HangUP();            
        }
    }
}
