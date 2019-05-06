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
    public partial class ExtendedControl : UserControl
    {
        public ExtendedControl()
        {
            InitializeComponent();
        }

        #region dependency properties
        // Dependency Property
        public static readonly DependencyProperty TextFontSizeProperty =
                DependencyProperty.Register(nameof(TextFontSize), typeof(int),
                typeof(ExtendedControl),
                new FrameworkPropertyMetadata(4,
                     TextFontSizeChanged, // optional
                     TextFontSizeCoerce), // optional
                     TextFontSizeValidate);  // optional

        public int TextFontSize
        {
            get { return (int)GetValue(TextFontSizeProperty); }
            set { SetValue(TextFontSizeProperty, value); }
        }
        private static void TextFontSizeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as ExtendedControl).SetMyTextSize((int)e.NewValue);
        }

        private static object TextFontSizeCoerce(DependencyObject sender, object data)
        {
            if ((int)data > 20)
            {
                data = 20;
            }
            return data;
        }

        private static bool TextFontSizeValidate(object data)
        {
            return (int)data < 20;
        }
        #endregion dependency properties

        public void SetMyTextSize(int size)
        {
            someTB.FontSize = size;
        }
    }
}
