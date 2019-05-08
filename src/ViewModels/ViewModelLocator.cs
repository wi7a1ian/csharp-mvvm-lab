using Unity;

namespace MvvmSampleApp.ViewModels
{
    public class ViewModelLocator
    {
        public ShellViewModel ShellViewModel => DiConfig.GetContainer().Resolve<ShellViewModel>();
        public MainWindowViewModel MainWindowViewModel => DiConfig.GetContainer().Resolve<MainWindowViewModel>();
        public SomeSubViewModel WithOwnVmControlViewModel => DiConfig.GetContainer().Resolve<SomeSubViewModel>();
    }
}
