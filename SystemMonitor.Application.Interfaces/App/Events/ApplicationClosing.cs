using Prism.Events;

namespace SystemMonitor.Application.Interfaces.App.Events
{
    public class ApplicationClosing
        : PubSubEvent<ApplicationClosingEventArgs>
    {
    }
}
