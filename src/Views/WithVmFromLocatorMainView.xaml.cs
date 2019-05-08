using MvvmSampleApp.ViewModels;
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

namespace MvvmSampleApp.Views
{
    public partial class WithVmFromLocatorMainView : UserControl
    {
        public MainWindowViewModel Model
        {
            get { return (FindName("root") as FrameworkElement)?.DataContext as MainWindowViewModel; }
        }

        public WithVmFromLocatorMainView()
        {
            InitializeComponent();
            
            Loaded += (_, __) => { Model.Loaded(); };
            Unloaded += (_, __) => { Model.Unloaded(); };
        }
    }
}
