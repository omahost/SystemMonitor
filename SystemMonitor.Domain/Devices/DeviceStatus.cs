using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Domain.Devices
{
    public class DeviceStatus : IDeviceStatus
    {
        public DeviceStatusType Type { get; }
        public string Description { get; }

        public DeviceStatus(DeviceStatusType type, string description = null)
        {
            Type = type;
            Description = description ?? string.Empty;
        }
    }
}
