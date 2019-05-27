using System;
using Unity;
using Unity.Injection;
using MvvmSampleApp.Core;
using MvvmSampleApp.Infrastructure;
using MvvmSampleApp.ViewModels;
using MvvmSampleApp.Views;
using MvvmSampleApp.Infrastructure.Wpf;
using MvvmSampleApp.Services;
using Unity.Lifetime;

namespace MvvmSampleApp
{
    public static class DiConfig
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetContainer()
        {
            return Container.Value;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            // repos:
            container.RegisterType<IItemsRepository, ItemsRepository>();

            // services:
            container.RegisterType<ISomeMediator, SimpleVmMediator>(new ContainerControlledLifetimeManager());

            // viewmodels:

            container.RegisterType<WithVmAsStaticResMainView>();
            container.RegisterType<ShellViewModel>();
            if(ViewModelBase.IsInDesignMode())
            {
                container.RegisterType<MainWindowViewModel>(new InjectionConstructor());
            }
            else
            {
                container.RegisterType<MainWindowViewModel>(new InjectionConstructor(typeof(IItemsRepository), typeof(ISomeMediator)));
            }

            // other
            // from file: unityContainer.LoadConfiguration();
            ConfigureInjectionFactories(container);
        }

        private static void ConfigureInjectionFactories(IUnityContainer container)
        {
            // This is where you can define your factory. It is not available in XML config in Unity IoC container, so it has to be done here, in code. 
            // This means you can't inject your own factory implementation during deployment, but you still can inject implementation of factory product.
            //unityContainer.RegisterType<Func<IBlobReader>>(
            //    new InjectionFactory(c => new Func<IBlobReader>(() => c.Resolve<IBlobReader>()))
            //);
        }
    }
}
