using Unity;
using MvvmSampleApp.ViewModels.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmSampleApp.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => DiConfig.GetContainer().Resolve<MainWindowViewModel>();
        public WithOwnVmControlViewModel WithOwnVmControlViewModel => DiConfig.GetContainer().Resolve<WithOwnVmControlViewModel>();
    }
}
