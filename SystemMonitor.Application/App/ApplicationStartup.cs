using SystemMonitor.Application.Interfaces.App;
using SystemMonitor.Application.Interfaces.Devices;
using SystemMonitor.UI.Interfaces;

namespace SystemMonitor.Application
{
    public class ApplicationStartup : IApplicationStartup
    {
        private readonly IMainWindow _mainWindow;
        private readonly IDeviceMonitorFacade _deviceMonitorFacade;

        public ApplicationStartup(
            IMainWindow mainWindow,
            IDeviceMonitorFacade deviceMonitorFacade
            )
        {
            _mainWindow = mainWindow;
            _deviceMonitorFacade = deviceMonitorFacade;
        }

        public void Run()
        {
            _mainWindow.Show();
            _deviceMonitorFacade.Start();
        }
    }
}
