using Prism.Events;
using System.ComponentModel;
using SystemMonitor.Application.Interfaces.App;
using SystemMonitor.Application.Interfaces.App.Events;
using SystemMonitor.Application.Interfaces.Devices;
using SystemMonitor.UI.Interfaces;

namespace SystemMonitor.Application
{
    public class ApplicationStartup : IApplicationStartup
    {
        private readonly IMainWindow _mainWindow;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDeviceMonitorFacade _deviceMonitorFacade;

        public ApplicationStartup(
            IMainWindow mainWindow,
            IEventAggregator eventAggregator,
            IDeviceMonitorFacade deviceMonitorFacade
            )
        {
            _mainWindow = mainWindow;
            _eventAggregator = eventAggregator;
            _deviceMonitorFacade = deviceMonitorFacade;
            _mainWindow.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            _mainWindow.Closing -= MainWindow_Closing;

            _eventAggregator
                .GetEvent<ApplicationClosing>()
                .Publish(new ApplicationClosingEventArgs());
        }

        public void Run()
        {
            _mainWindow.Show();
            _deviceMonitorFacade.Start();
        }
    }
}
