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

namespace MvvmSampleApp.Views
{
    public partial class SomeSubView : UserControl
    {
        public SomeSubViewModel Model
        {
            set { Resources["ViewModel"] = value; }
            get { return (SomeSubViewModel)Resources["ViewModel"]; }
        }

        public SomeSubView()
        {
            InitializeComponent();
        }

        #region dependency properties

        public static readonly DependencyProperty TextFontSizeProperty =
                DependencyProperty.Register(nameof(TextFontSize), typeof(int),
                typeof(SomeSubView),
                new FrameworkPropertyMetadata(4, TextFontSizeChanged));

        public int TextFontSize
        {
            get { return (int)GetValue(TextFontSizeProperty); }
            set { SetValue(TextFontSizeProperty, value); }
        }
        private static void TextFontSizeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as SomeSubView).SetMyTextSize((int)e.NewValue);
        }

        #endregion dependency properties

        public void SetMyTextSize(int size)
        {
            Model.MyFontSize = size;
        }
    }
}
