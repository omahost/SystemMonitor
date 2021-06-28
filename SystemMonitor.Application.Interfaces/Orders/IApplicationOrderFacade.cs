using System.Collections.Generic;
using SystemMonitor.Domain.Interfaces.Orders;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Application.Interfaces.Orders
{
    public interface IApplicationOrderFacade
       : ISingletonDependency
    {
        IList<IApplicationOrder> GetOrders();
        void SetOrderAsCanceled(IApplicationOrder order);
        void SetOrderAsPrinted(IApplicationOrder order);
    }
}
