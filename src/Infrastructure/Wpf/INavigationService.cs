using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MvvmSampleApp.Infrastructure.Wpf
{
    public class NavigateEventArgs : EventArgs
    {
        public string Path { get; set; }
        public FrameworkElement View { get; set; }
    }

    public interface INavigationService
    {
        event EventHandler<NavigateEventArgs> Navigating;

        void RegisterView<TView>(string path)
            where TView : FrameworkElement, new();

        void NavigateTo(string path);
    }
}
