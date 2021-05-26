using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Application.Interfaces.Devices
{
    public interface IDeviceMonitorFacade 
        : IDeviceMonitor
        , ISingletonDependency
    {
    }
}
