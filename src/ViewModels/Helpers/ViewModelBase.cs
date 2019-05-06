﻿using System.Windows;

namespace MvvmSampleApp.ViewModels.Helpers
{
    public class ViewModelBase : BindableBase
    {
        public static bool IsInDesignMode()
        {
            return (Application.Current.MainWindow == null);
        }
    }
}