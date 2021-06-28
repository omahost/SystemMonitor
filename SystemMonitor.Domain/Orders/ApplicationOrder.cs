using System;
using System.Collections.Generic;
using SystemMonitor.Domain.Interfaces.Orders;
using SystemMonitor.Domain.Interfaces.Tasks;

namespace SystemMonitor.Domain.Orders
{
    /// <summary>
    /// For the test task this is should be acceptable
    /// For real app, should incapsulate/restrict direct field manipulation
    /// </summary>
    public class ApplicationOrder : IApplicationOrder
    {
        public int Id { get; set; }
        public string OrderNote { get; set; }
        public string Waiter { get; set; }
        public IApplicationTask Task { get; set; }
        public ApplicationTaskStatus State { get; set; }
        public string TableNumber { get; set; }
        public int CustomersCount { get; set; }
        public DateTime OrderedAt { get; set; }
        public ApplicationOrderType OrderType { get; set; }
        public IList<IApplicationOrderItem> Items { get; set; } 
            = new List<IApplicationOrderItem>();
    }
}
