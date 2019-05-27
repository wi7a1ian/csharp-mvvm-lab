using System.Windows;
using Unity;
using MvvmSampleApp.Views;
using System;
using System.Threading.Tasks;

namespace MvvmSampleApp
{
    public partial class App : Application
    {
        public App()
        {
            Current.DispatcherUnhandledException += (sender, e) =>
            {
                // log
                e.Handled = true; // Prevent default unhandled exception processing
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                // log
            };

            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                // log
            };
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow = DiConfig.GetContainer().Resolve<Shell>();
            MainWindow.Show();
        }
    }
}
