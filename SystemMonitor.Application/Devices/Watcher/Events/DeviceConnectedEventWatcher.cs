namespace SystemMonitor.Application.Devices
{
    public class DeviceConnectedEventWatcher : DeviceInstanceEventWatcher
    {
        public DeviceConnectedEventWatcher(string wmiClass)
            : base("__InstanceCreationEvent", wmiClass)
        {
        }
    }
}
