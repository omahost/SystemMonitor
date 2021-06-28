using System;
using SystemMonitor.Domain.Interfaces.Orders;

namespace SystemMonitor.Control.Receipt.Interfaces.Events
{
    public class ApplicationOrderPrintedEventArgs : EventArgs
    {
        public IApplicationOrder Order { get; }

        public ApplicationOrderPrintedEventArgs(IApplicationOrder order)
        {
            Order = order;
        }
    }
}
