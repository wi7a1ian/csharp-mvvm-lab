using System.Linq;
using System.Collections.Generic;
using MvvmSampleApp.Core;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using MvvmSampleApp.Models;
using MvvmSampleApp.Infrastructure.Wpf;
using MvvmSampleApp.Services;
using System.Windows.Controls;
using System;

namespace MvvmSampleApp.ViewModels
{
    public class FontSizeChangeViewModel : ViewModelBase
    {
        private readonly IFontTransformationMediator someMediator;

        #region props

        private int activeFontSize = 16;
        public int ActiveFontSize
        {
            get { return activeFontSize; }
            set { SetProperty(ref activeFontSize, value); }
        }

        #endregion

        #region commands

        public ICommand ChangeFontSizeCommand { get; private set; }

        #endregion
        
        public FontSizeChangeViewModel()
        {
            // nop
        }

        public FontSizeChangeViewModel(IItemsRepository itemsRepository, IFontTransformationMediator someMediator)
        {
            this.someMediator = someMediator;
            
            ConfigureSomeMediator();
        }

        private void ConfigureSomeMediator()
        {
            ChangeFontSizeCommand = someMediator.ChangeMainFontCommand;
            someMediator.SomethingChanged += (o, e) => { ActiveFontSize = e.FontSize; };
        }
    }
}
