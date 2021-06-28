using System.Threading.Tasks;
using SystemMonitor.Domain.Interfaces;
using SystemMonitor.Domain.Interfaces.Orders;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Control.Receipt.Interfaces
{
    public interface IReceiptPrinterFacade
        : ISingletonDependency
    {
        Task PrintAsync(IApplicationOrder order, IDeviceInfo deviceInfo);
    }
}
