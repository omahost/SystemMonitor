using System.Collections.Generic;
using SystemMonitor.Application.Orders.Dto;
using SystemMonitor.Domain.Interfaces.Orders;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Application.Orders
{
    public interface IApplicationOrderApiMapper : ISingletonDependency
    {
        List<IApplicationOrder> Map(NewOrdersDto newOrdersDto);
    }
}
