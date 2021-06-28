using System;
using System.Collections.Generic;
using SystemMonitor.Domain.Interfaces.Tasks;

namespace SystemMonitor.Domain.Interfaces.Orders
{
    public interface IApplicationOrder
    {
        int Id { get; }
        string OrderNote { get; }
        string Waiter { get; }
        string TableNumber { get; }
        int CustomersCount { get; }
        DateTime OrderedAt { get; }
        ApplicationOrderType OrderType { get; }
        IApplicationTask Task { get; }
        ApplicationTaskStatus State { get; set; }
        IList<IApplicationOrderItem> Items { get; }
    }
}
