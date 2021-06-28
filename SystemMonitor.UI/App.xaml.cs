using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using SystemMonitor.Application;
using SystemMonitor.Application.Interfaces.App;
using SystemMonitor.Domain;
using SystemMonitor.Infrastructure;
using SystemMonitor.Infrastructure.Interfaces.Modules;
using SystemMonitor.Interfaces.Ioc;
using SystemMonitor.UI.Interfaces;
using Unity;

namespace SystemMonitor.UI
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            var window = Container
                .Resolve<IMainWindow>()
                .GetWindow();

            return window;
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName;
                viewName = viewName.Replace(".Views.", ".ViewModels.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
                var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}{1}, {2}", viewName, suffix, viewAssemblyName);
                var viewModelType = Type.GetType(viewModelName);
                return viewModelType;
            });
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            Container
                .Resolve<IModuleLoader>()
                .LoadModules(moduleCatalog)
                ;
                
            Container.Initialize();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterContainerRegistry(containerRegistry);
            RegisterUnityContainer(containerRegistry);

            Container
                .AddInfrastructure(containerRegistry)
                .AddDomain()
                .AddApplication()
                .AddUI()
                ;
        }

        private void RegisterContainerRegistry(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(containerRegistry);
        }

        private void RegisterUnityContainer(IContainerRegistry containerRegistry)
        {
            var container = containerRegistry.GetContainer();
            container.AddExtension(new Diagnostic());

            containerRegistry.RegisterInstance(Container);
            containerRegistry.RegisterInstance(container);
        }

        protected override void OnInitialized()
        {
            // to avoid showing main window, make it null
            var mainWindow = MainWindow;
            MainWindow = null;

            base.OnInitialized();

            // restore original reference
            MainWindow = mainWindow;

            // now run main logic
            Container
                .Resolve<IApplicationStartup>()
                .Run();
        }
    }
}
