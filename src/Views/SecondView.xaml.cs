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
using MvvmSampleApp.ViewModels;
using Unity;

namespace MvvmSampleApp.Views
{
    /// <summary>
    /// Interaction logic for FirstSampleControl.xaml
    /// </summary>
    public partial class FirstSampleView : UserControl
    {
        [Dependency]
        public SecondViewModel Model
        {
            set { Resources["ViewModel"] = value; }
            get { return (SecondViewModel)Resources["ViewModel"]; }
        }

        public FirstSampleView()
        {
            InitializeComponent();
            Loaded += (_, __) => { Model.Loaded(); };
            Unloaded += (_, __) => { Model.Unloaded(); };
        }
    }
}
