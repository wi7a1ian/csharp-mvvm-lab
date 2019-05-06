using System.Windows;

namespace MvvmSampleApp.ViewModels.Helpers
{
    public class BaseViewModel : BindableBase
    {
        public static bool IsInDesignMode()
        {
            return (Application.Current.MainWindow == null);
        }
    }
}
