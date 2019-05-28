using MvvmSampleApp.Infrastructure.Wpf;
using MvvmSampleApp.ViewModels;
using MvvmSampleApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;

namespace MvvmSampleApp
{
    public partial class Shell : Window
    {
        public Shell(INavigationService navigator)
        {
            InitializeComponent();
            RegisterViews(navigator);
        }

        private void RegisterViews(INavigationService navigator)
        {
            navigator.RegisterView<FontSizeChangeView>("view1");
            navigator.RegisterView<SomeSubView>("view2");
            navigator.RegisterView<WithVmFromLocatorMainView>("view3");
        }
    }
}
