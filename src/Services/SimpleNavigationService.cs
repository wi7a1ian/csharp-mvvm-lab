using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MvvmSampleApp.Infrastructure.Wpf;

namespace MvvmSampleApp.Services
{
    public class SimpleNavigationService : INavigationService
    {
        private readonly static DependencyObject dummy = new DependencyObject();
        public static Dispatcher UIDispatcher => (dummy.Dispatcher != null && dummy.Dispatcher.Thread.IsAlive) ? dummy.Dispatcher : null;

        private readonly IDictionary<string, Lazy<FrameworkElement>> registeredViews 
            = new Dictionary<string, Lazy<FrameworkElement>>();

        public event EventHandler<NavigateEventArgs> Navigating;

        public void RegisterView<TView>(string path)
            where TView : FrameworkElement, new()
        {
            registeredViews.Add(path.ToLower(), new Lazy<FrameworkElement>(() => new TView()));
        }

        public void NavigateTo(string path)
        {
            path = path.ToLower();
            if (registeredViews.ContainsKey(path) && UIDispatcher != null)
            {
                if (UIDispatcher.CheckAccess())
                {
                    LoadAndNavigateTo(path);
                }
                else
                {
                    UIDispatcher.BeginInvoke((Action)(() => LoadAndNavigateTo(path)));
                }
            }
        }

        private void LoadAndNavigateTo(string path)
        {
            var view = registeredViews[path]?.Value;
            if (view != null)
            {
                Navigating?.Invoke(this, new NavigateEventArgs { Path = path,  View = view });
            }
        }
    }
}
