using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIPManagementNewMVVM.Model.DataModel;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.Home
{
    internal class CallBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;
        HomeViewModel HomeViewModel { get; set; }
        public CallBTN(HomeViewModel homeViewModel)
        {
            HomeViewModel=homeViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            List<NumberModel> numberModels = new List<NumberModel>();
            numberModels.Add(HomeViewModel.NumberModel);
            HomeViewModel._NumberModels = numberModels;
            HomeViewModel.OzekiModels.Data = HomeViewModel.NumberModel;
            HomeViewModel.OzekiModels.Call(ref numberModels);
        }
    }
}
