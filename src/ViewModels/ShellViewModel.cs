using System.Linq;
using System.Collections.Generic;
using MvvmSampleApp.Core;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using MvvmSampleApp.Models;
using MvvmSampleApp.Infrastructure.Wpf;

namespace MvvmSampleApp.ViewModels
{

    public class ShellViewModel : ViewModelBase
    {
        private readonly INavigationService navigator;

        public ICommand ActivateViewCommand { get; private set; }

        private FrameworkElement activeView = new FrameworkElement();
        public FrameworkElement ActiveView
        {
            get { return activeView; }
            set { SetProperty(ref activeView, value); }
        }

        public ShellViewModel( INavigationService navigator )
        {
            this.navigator = navigator;
            ConfigureNavigation();
        }

        private void ConfigureNavigation()
        {
            ActivateViewCommand = new RelayCommand<string>(
                name => navigator.NavigateTo(name),
                canExecute => true);

            navigator.Navigating += (o, e) => ActiveView = e.View;
        }
    }
}
