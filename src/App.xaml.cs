using MvvmSampleApp.Views;
using MvvmSampleApp.Core;
using MvvmSampleApp.Infrastructure;
using MvvmSampleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Injection;

namespace MvvmSampleApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            ConfigureUnityDiContainer(container);

            MainWindow = container.Resolve<MainWindow>(); ;
            MainWindow.Show();
        }

        private void ConfigureUnityDiContainer(IUnityContainer container)
        {
            // repos:
            container.RegisterType<IFirstSampleItemsRepository, FirstSampleItemsRepository>();

            // services:
            // none

            // viewmodels:
            container.RegisterType<FirstSampleViewModel>(new InjectionConstructor(typeof(IFirstSampleItemsRepository)));

            // views
            container.RegisterType<FirstSampleView>(new InjectionProperty("Model", new ResolvedParameter<FirstSampleViewModel>()));
        }
    }
}
