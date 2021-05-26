namespace SystemMonitor.Application.Devices
{
    public class DeviceInstanceEventWatcher : DeviceEventWatcher
    {
        public DeviceInstanceEventWatcher(string eventName, string wmiClass)
            : base($"SELECT * FROM {eventName} WITHIN 2 WHERE TargetInstance ISA '{wmiClass}'")
        {
        }
    }
}
