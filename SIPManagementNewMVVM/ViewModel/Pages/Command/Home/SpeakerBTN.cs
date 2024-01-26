using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIPManagementNewMVVM.ViewModel.Pages.Command.Home
{
    internal class SpeakerBTN : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public HomeViewModel HomeViewModel { get; set; }
        public SpeakerBTN(HomeViewModel homeViewModel)
        {
            HomeViewModel=homeViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            HomeViewModel.OzekiModels.OpenSpeaker();
        }
    }
}
