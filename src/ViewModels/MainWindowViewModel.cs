using System.Linq;
using System.Collections.Generic;
using MvvmSampleApp.Core;
using MvvmSampleApp.ViewModels.Helpers;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;

namespace MvvmSampleApp.ViewModels
{
    // TODO: switch to another view (subview)
    // TODO: service locator pattern for injecting viewmodels (avoid DI) - http://dotnetpattern.com/mvvm-light-toolkit-example

    public class MainWindowViewModel : ViewModelBase
    {
        private bool isLoaded = false;
        private readonly IItemsRepository itemsRepository;

        #region subviewmodels

        public class SomeSubViewModel : ViewModelBase
        {
            private int selectedSubFontSize = 16;
            public int SelectedSubFontSize
            {
                get { return selectedSubFontSize; }
                set { SetProperty(ref selectedSubFontSize, value); }
            }

            public string SomeTextFromChildControl
            {
                set { MessageBox.Show(value); }
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


        public MainWindowViewModel(IItemsRepository itemsRepository)
        {
            this.itemsRepository = itemsRepository;

            ConfigureCommands();
        }

        public void Loaded()
        {
            if (!isLoaded)
            {
                foreach (var i in itemsRepository.GetItems(10)) Items.Add(i);

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

        private void ConfigureCommands()
        {
            ChangeFontSizeCommand = new RelayCommand<string>(
                /*async*/ direction => { SelectedFontSize += (direction.Equals("+")) ? 1 : -1; SubViewModel.SelectedSubFontSize = SelectedFontSize; },
                _ => true);
        }

    }
}
