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

namespace MvvmSampleApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private bool isLoaded = false;
        private readonly IItemsRepository itemsRepository;
        private readonly ISomeMediator someMediator;

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

        public ICommand ChangeFontSizeCommand { get; private set; }
        public ICommand ChangeFontSizeForAllCommand { get; private set; }

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


        public MainWindowViewModel(IItemsRepository itemsRepository, ISomeMediator someMediator)
        {
            this.itemsRepository = itemsRepository;
            this.someMediator = someMediator;

            ConfigureCommands();
            ConfigureSomeMediator();
        }

        private void ConfigureCommands()
        {
            ChangeFontSizeCommand = new RelayCommand<string>(
                /*async*/ direction => { SelectedFontSize += (direction.Equals("+")) ? 1 : -1; SubViewModel.SelectedSubFontSize = SelectedFontSize; },
                _ => true);

            ChangeFontSizeForAllCommand = new RelayCommand<string> (
                direction => { someMediator.OnChange(direction); },
                _ => true);
        }

        private void ConfigureSomeMediator()
        {
            if (someMediator.SomeCommand == null)
            {
                // Im first! Register my command in the mediator.
                someMediator.SomeCommand = this.ChangeFontSizeForAllCommand;
            }
            else
            {
                // My "plus"/"minus" btn will call cmd from the first VM that registered itself
                this.ChangeFontSizeCommand = someMediator.SomeCommand;
            }

            // Everyone will change font size when requested
            someMediator.SomethingChanged += (o, e) => { SelectedFontSize += (e.Something.Equals("+")) ? 1 : -1; SubViewModel.SelectedSubFontSize = SelectedFontSize; };
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
