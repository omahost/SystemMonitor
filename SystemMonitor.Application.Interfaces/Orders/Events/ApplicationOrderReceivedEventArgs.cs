using System;
using System.Collections.Generic;
using SystemMonitor.Domain.Interfaces.Orders;

namespace SystemMonitor.Application.Interfaces.Orders.Events
{
    public class ApplicationOrderReceivedEventArgs : EventArgs
    {
        public IReadOnlyList<IApplicationOrder> Orders { get; }

        public ApplicationOrderReceivedEventArgs(List<IApplicationOrder> orders)
        {
            Orders = orders.AsReadOnly();
        }
    }
}
