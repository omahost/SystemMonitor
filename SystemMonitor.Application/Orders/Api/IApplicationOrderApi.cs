using System.Threading;
using System.Threading.Tasks;
using SystemMonitor.Application.Orders.Dto;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Application.Orders
{
    public interface IApplicationOrderApi : ISingletonDependency
    {
        Task SetOrderAsCanceledAsync(int orderId);
        Task SetOrderAsPrintedAsync(int orderId);
        Task<NewOrdersDto> PollNewOrdersAsync(CancellationToken cancellationToken);
    }
}
