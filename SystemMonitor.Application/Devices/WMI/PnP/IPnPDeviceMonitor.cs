using SystemMonitor.Application.Interfaces.Devices;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Application.Devices
{
    public interface IPnPDeviceMonitor 
        : IDeviceMonitor
        , ISingletonDependency
    {
    }
}
