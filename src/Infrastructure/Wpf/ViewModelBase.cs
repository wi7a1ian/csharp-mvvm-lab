using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace MvvmSampleApp.Infrastructure.Wpf
{
    public class ViewModelBase : BindableBase
    {
        private readonly static DependencyObject dummy = new DependencyObject();

        public static bool IsInDesignMode() => DesignerProperties.GetIsInDesignMode(dummy);

        public static Dispatcher UIDispatcher => (dummy.Dispatcher != null && dummy.Dispatcher.Thread.IsAlive) ? dummy.Dispatcher : null;

        public static bool TryDispatch(Action action)
        {
            if (action != null && UIDispatcher != null)
            {
                if (dummy.Dispatcher.CheckAccess())
                {
                    action();
                }
                else
                {
                    UIDispatcher.BeginInvoke(DispatcherPriority.Normal, action);
                }
                return true;
            }

            return (UIDispatcher != null);
        }
    }
}
