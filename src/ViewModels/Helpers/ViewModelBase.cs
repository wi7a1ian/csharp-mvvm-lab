using System.ComponentModel;
using System.Windows;

namespace MvvmSampleApp.ViewModels.Helpers
{
    public class ViewModelBase : BindableBase
    {
        private readonly static DependencyObject dummy = new DependencyObject();
        public static bool IsInDesignMode() => DesignerProperties.GetIsInDesignMode(dummy);
    }
}
