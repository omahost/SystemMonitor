using SystemMonitor.Domain.Interfaces;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Control.Receipt.Interfaces
{
    public interface IReceiptPrinterFacade
        : ISingletonDependency
    {
        void Print(IDeviceInfo deviceInfo);
    }
}
