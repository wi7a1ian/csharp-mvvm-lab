﻿using MvvmSampleApp.ViewModels;
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
    public partial class SampleControl : UserControl
    {
        public SampleControl()
        {
            InitializeComponent();
        }

        #region dependency properties

        public static readonly DependencyProperty TextFontSizeProperty =
                DependencyProperty.Register(nameof(TextFontSize), typeof(int),
                typeof(SampleControl),
                new FrameworkPropertyMetadata(4, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public int TextFontSize
        {
            get { return (int)GetValue(TextFontSizeProperty); }
            set { SetValue(TextFontSizeProperty, value); }
        }

        #endregion
    }
}
