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
    public partial class WithVmAsStaticResMainView : UserControl
    {
        [Dependency] // broken if View is embedded in XAML, gave to do container.Resolve<>() ...
        public MainWindowViewModel Model
        {
            set { Resources["ViewModel"] = value; }
            get { return (MainWindowViewModel)Resources["ViewModel"]; }
        }

        public WithVmAsStaticResMainView()
        {
            InitializeComponent();

            Loaded += (_, __) => { Model.Loaded(); };
            Unloaded += (_, __) => { Model.Unloaded(); };
        }
    }
}
