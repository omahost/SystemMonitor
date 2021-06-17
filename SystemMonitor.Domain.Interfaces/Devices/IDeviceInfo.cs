namespace SystemMonitor.Domain.Interfaces
{
    public interface IDeviceInfo 
    {
        DeviceType Type { get; }
        bool IsRemote { get; }
        IDeviceStatus Status { get; }
        string ServerPath { get; }
        string DeviceId { get; }
        string PNPDeviceId { get; }
        string Name { get; }
        string Description { get; }
        string Caption { get; }
    }
}
