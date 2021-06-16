using System.Windows.Media;
using SystemMonitor.Interfaces.Ioc;
using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Infrastructure.Interfaces.Printing
{
    public interface IPrinterFacade : ISingletonDependency
    {
        void Print(Visual visual, IDeviceInfo device, string description);
    }
}
