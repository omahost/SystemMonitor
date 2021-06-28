using System;
using SystemMonitor.Domain.Interfaces.Orders;

namespace SystemMonitor.Application.Interfaces.Orders.Events
{
    public class ApplicationOrderStateChangedEventArgs : EventArgs
    {
        public IApplicationOrder Order { get; }

        public ApplicationOrderStateChangedEventArgs(IApplicationOrder order)
        {
            Order = order;
        }
    }
}
