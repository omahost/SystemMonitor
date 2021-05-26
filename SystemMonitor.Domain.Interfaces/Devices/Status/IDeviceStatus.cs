namespace SystemMonitor.Domain.Interfaces
{
    public interface IDeviceStatus
    {
        public DeviceStatusType Type { get; }
        public string Description { get; }
    }
}
