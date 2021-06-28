using Prism.Events;

namespace SystemMonitor.Control.Receipt.Interfaces.Events
{
    public class ApplicationOrderPrinted
        : PubSubEvent<ApplicationOrderPrintedEventArgs>
    {
    }
}
