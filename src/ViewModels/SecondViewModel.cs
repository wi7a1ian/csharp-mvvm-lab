using System.Linq;
using System.Collections.Generic;
using MvvmSampleApp.Core;
using MvvmSampleApp.ViewModels.Helpers;

namespace MvvmSampleApp.ViewModels
{
    public class SecondViewModel : BindableBase
    {
        private bool isLoaded = false;

        // TODO: dependency prop https://gist.github.com/wi7a1ian/6c142e238e89458f70e7d8cdcb890f1c  https://gist.github.com/wi7a1ian/bb84bd1ffecbbe80385da1658055fdfb

        public SecondViewModel()
        {
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
