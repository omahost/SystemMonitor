using Prism.Events;

namespace SystemMonitor.Application.Interfaces.Orders.Events
{
    public class ApplicationOrderReceived
        : PubSubEvent<ApplicationOrderReceivedEventArgs>
    {
    }
}
