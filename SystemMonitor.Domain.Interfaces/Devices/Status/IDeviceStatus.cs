namespace SystemMonitor.Domain.Interfaces
{
    public interface IDeviceStatus
    {
        DeviceStatusType Type { get; }
        string Description { get; }
    }
}
