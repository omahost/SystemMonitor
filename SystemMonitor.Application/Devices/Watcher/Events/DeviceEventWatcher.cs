using System.Management;

namespace SystemMonitor.Application.Devices
{
    public class DeviceEventWatcher
    {
        private readonly ManagementEventWatcher _eventWatcher;
        public DeviceEventWatcher(string query)
        {
            _eventWatcher = new ManagementEventWatcher(query);
            _eventWatcher.EventArrived += EventWatcher_EventArrived;
        }

        public void Start()
        {
            _eventWatcher.Start();
        }

        private void EventWatcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            EventArrived?.Invoke(this, e);
        }

        public event EventArrivedEventHandler EventArrived;
    }
}
