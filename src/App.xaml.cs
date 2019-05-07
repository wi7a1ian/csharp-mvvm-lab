using System.Windows;
using Unity;
using MvvmSampleApp.Views;

namespace MvvmSampleApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow = DiConfig.GetContainer().Resolve<MainWindow>();
            //MainWindow = DiConfig.GetContainer().Resolve<MainWindowWithVmFromLocator>();

            MainWindow.Show();
        }
    }
}
