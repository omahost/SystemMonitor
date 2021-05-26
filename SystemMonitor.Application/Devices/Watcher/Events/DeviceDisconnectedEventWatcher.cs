namespace SystemMonitor.Application.Devices
{
    public class DeviceDisconnectedEventWatcher : DeviceInstanceEventWatcher
    {
        public DeviceDisconnectedEventWatcher(string wmiClass)
            : base("__InstanceDeletionEvent", wmiClass)
        {
        }
    }
}
