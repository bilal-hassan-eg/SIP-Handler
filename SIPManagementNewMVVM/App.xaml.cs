using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SIPManagementNewMVVM.View.Windows;
using SIPManagementNewMVVM.ViewModel.Windows;
namespace SIPManagementNewMVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            /*
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

            if (principal.IsInRole(WindowsBuiltInRole.Administrator) == false && principal.IsInRole(WindowsBuiltInRole.User) == true)
            {
                ProcessStartInfo objProcessInfo = new ProcessStartInfo();
                objProcessInfo.UseShellExecute = true;
                objProcessInfo.FileName = Assembly.GetEntryAssembly().CodeBase;
                objProcessInfo.UseShellExecute = true;
                objProcessInfo.Verb = "runas";
                try
                {
                    Process proc = Process.Start(objProcessInfo);
                    Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                }
            }*/
            MainWindow mainWindow = new MainWindow();
            mainWindowViewModel mainWindowViewModel = new mainWindowViewModel();
            mainWindow.DataContext = mainWindowViewModel;
            mainWindowViewModel.setContext(mainWindow);
            mainWindow.Show();
        }
    }
}
