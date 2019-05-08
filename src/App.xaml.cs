using System.Windows;
using Unity;
using MvvmSampleApp.Views;

namespace MvvmSampleApp
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow = DiConfig.GetContainer().Resolve<Shell>();
            MainWindow.Show();
        }
    }
}
