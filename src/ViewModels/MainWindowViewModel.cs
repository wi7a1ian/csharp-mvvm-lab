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
    public class MainWindowViewModel : ViewModelBase
    {
        private bool isLoaded = false;
        private readonly IItemsRepository itemsRepository;
        private readonly IFontTransformationMediator someMediator;

        #region subviewmodels

        public class SomeSubViewModel : ViewModelBase
        {
            private int selectedSubFontSize = 16;
            public int SelectedSubFontSize
            {
                get { return selectedSubFontSize; }
                set { SetProperty(ref selectedSubFontSize, value); }
            }

            public SomePassedBetweenCtrlsModel SomeDataFromChildControl
            {
                set { MessageBox.Show(value.SecretText); }
            }
        }
        public SomeSubViewModel SubViewModel { get; } = new SomeSubViewModel();

        #endregion

        #region bindable properties/collactions

        public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();

        private int selectedFontSize = 16;
        public int SelectedFontSize
        {
            get { return selectedFontSize; }
            set { SetProperty(ref selectedFontSize, value); }
        }

        #endregion

        #region commands

        public ICommand ActivateViewCommand { get; private set; }

        public ICommand ChangeFontSizeCommand { get; private set; }

        #endregion
        
        public MainWindowViewModel()
        {
            ConfigureCommands();

            if (IsInDesignMode())
            {
                var fakeItems = new List<string> { "Item A", "Item B", "Item C", "Item D", "Item E" };
                foreach (var i in fakeItems) Items.Add(i);
            }
        }


        public MainWindowViewModel(
            IItemsRepository itemsRepository, 
            IFontTransformationMediator someMediator)
        {
            this.itemsRepository = itemsRepository;
            this.someMediator = someMediator;

            ConfigureCommands();
            ConfigureSomeMediator();
        }

        private void ConfigureCommands()
        {
            ChangeFontSizeCommand = new RelayCommand<string>( /*async*/
                direction => { 
                    SelectedFontSize += (direction.Equals("+")) ? 1 : -1; SubViewModel.SelectedSubFontSize = SelectedFontSize; 
                    someMediator?.OnChangeFontSize(SelectedFontSize); },
                _ => true);
        }

        private void ConfigureSomeMediator()
        {
            if (someMediator.ChangeMainFontCommand == null)
            {
                // Im first! Register my command in the mediator.
                // Everyone will be able to change  font size when requested
                someMediator.ChangeMainFontCommand = this.ChangeFontSizeCommand;
            }
        }

        public void Loaded()
        {
            if (!isLoaded)
            {
                if(itemsRepository != null)
                {
                    foreach (var i in itemsRepository.GetItems(10)) Items.Add(i);
                }

                //PropertyChanged += (s,e) => MessageBox.Show((e as PropertyChangedEventArgs).PropertyName == nameof(SomeTextFromChildControl));

                isLoaded = true;
            }
        }

        public void Unloaded()
        {
            if (isLoaded)
            {
                Items.Clear();
                isLoaded = false;
            }
        }
    }
}
