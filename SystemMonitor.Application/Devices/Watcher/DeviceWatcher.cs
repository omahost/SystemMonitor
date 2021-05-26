using System.Management;

namespace SystemMonitor.Application.Devices
{
    public class DeviceWatcher
    {
        private DeviceConnectedEventWatcher _connected;
        private DeviceDisconnectedEventWatcher _disconnected;

        public DeviceWatcher(string wmiClass)
        {
            _connected = new DeviceConnectedEventWatcher(wmiClass);
            _connected.EventArrived += Connected_EventArrived;

            _disconnected = new DeviceDisconnectedEventWatcher(wmiClass);
            _disconnected.EventArrived += Disconnected_EventArrived;
        }

        public event EventArrivedEventHandler Disconnected;
        private void Disconnected_EventArrived(object sender, EventArrivedEventArgs e)
        {
            Disconnected?.Invoke(sender, e);
        }

        public event EventArrivedEventHandler Connected;
        private void Connected_EventArrived(object sender, EventArrivedEventArgs e)
        {
            Connected?.Invoke(sender, e);
        }

        public void Start()
        {
            _connected.Start();
            _disconnected.Start();
        }
    }
}
