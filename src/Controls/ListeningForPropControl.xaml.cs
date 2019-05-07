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

namespace MvvmSampleApp.Controls
{
    public partial class ListeningForPropControl : UserControl
    {
        public ListeningForPropControl()
        {
            InitializeComponent();
        }

        #region commands

        private void PressMe_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PressMe_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SomeText = "Howdy howdy I'm a cowboy";
        }

        #endregion

        #region dependency properties

        public static readonly DependencyProperty SomeTextProperty =
                DependencyProperty.Register(nameof(SomeText), typeof(string),
                typeof(ListeningForPropControl),
                new FrameworkPropertyMetadata("Howdy", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string SomeText
        {
            get { return (string)GetValue(SomeTextProperty); }
            set { SetValue(SomeTextProperty, value); }
        }

        #endregion
    }
}
