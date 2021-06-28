using Prism.Events;

namespace SystemMonitor.Application.Interfaces.Tasks.Events
{
    public class ApplicationTaskDeviceChanged
        : PubSubEvent<ApplicationTaskDeviceChangedEventArgs>
    {
    }
}
