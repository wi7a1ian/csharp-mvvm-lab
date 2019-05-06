using System.Linq;
using System.Collections.Generic;
using MvvmSampleApp.Core;
using MvvmSampleApp.ViewModels.Helpers;

namespace MvvmSampleApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private bool isLoaded = false;
        private readonly IItemsRepository itemsRepository;

        public IList<string> Items { get; set; }
        
        private int selectedFontSize = 16;
        public int SelectedFontSize
        {
            get { return selectedFontSize; }
            set { SetProperty(ref selectedFontSize, value); }
        }

        // TODO: fix DI not loading view
        // TODO: https://gist.github.com/wi7a1ian/4f2d650c0474f9f4c745b291f7bdb143
        // TODO: commands https://gist.github.com/wi7a1ian/28c042b64cfd26e8e3bb5de64c0d50f6
        // TODO: events https://gist.github.com/wi7a1ian/1eb34a2d1135cacc0af64106301f853b
        // TODO: dependency prop https://gist.github.com/wi7a1ian/6c142e238e89458f70e7d8cdcb890f1c  https://gist.github.com/wi7a1ian/bb84bd1ffecbbe80385da1658055fdfb

        public MainWindowViewModel()
        {
            if(IsInDesignMode())
            {
                Items = new List<string> { "Item A", "Item B", "Item C", "Item D", "Item E" };
            }
        }

        public MainWindowViewModel(IItemsRepository itemsRepository)
        {
            this.itemsRepository = itemsRepository;

            Items = itemsRepository.GetItems(10).ToList();
        }

        public void Loaded()
        {
            if (!isLoaded)
            {
                isLoaded = true;
            }
        }

        public void Unloaded()
        {
            if (isLoaded)
            {
                isLoaded = false;
            }
        }
    }
}
