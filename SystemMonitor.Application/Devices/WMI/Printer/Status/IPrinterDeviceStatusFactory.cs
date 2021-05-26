using System.Management;
using SystemMonitor.Domain.Interfaces;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Application.Devices
{
    public interface IPrinterDeviceStatusFactory :ISingletonDependency
    {
        IDeviceStatus Create(ManagementBaseObject managementDevice);
    }
}
