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

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public MainWindowViewModel Model
        {
            set { Resources["ViewModel"] = value; }
            get { return (MainWindowViewModel)Resources["ViewModel"]; }
        }

        public MainWindow()
        {
            InitializeComponent();

            Loaded += (_, __) => { Model.Loaded(); };
            Unloaded += (_, __) => { Model.Unloaded(); };
        }
    }
}
