using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using SystemMonitor.Application;
using SystemMonitor.Application.Interfaces.App;
using SystemMonitor.Domain;
using SystemMonitor.Infrastructure;
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
