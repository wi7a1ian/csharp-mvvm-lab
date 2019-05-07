using MvvmSampleApp.ViewModels.Controls;
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
    public partial class WithOwnVmControl : UserControl
    {
        public WithOwnVmControlViewModel Model
        {
            set { Resources["ViewModel"] = value; }
            get { return (WithOwnVmControlViewModel)Resources["ViewModel"]; }
        }

        public WithOwnVmControl()
        {
            InitializeComponent();
        }

        #region dependency properties

        public static readonly DependencyProperty TextFontSizeProperty =
                DependencyProperty.Register(nameof(TextFontSize), typeof(int),
                typeof(WithOwnVmControl),
                new FrameworkPropertyMetadata(4, TextFontSizeChanged));

        public int TextFontSize
        {
            get { return (int)GetValue(TextFontSizeProperty); }
            set { SetValue(TextFontSizeProperty, value); }
        }
        private static void TextFontSizeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as WithOwnVmControl).SetMyTextSize((int)e.NewValue);
        }

        #endregion dependency properties

        public void SetMyTextSize(int size)
        {
            Model.MyFontSize = size;
        }
    }
}
