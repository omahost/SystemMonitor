using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Domain.Devices
{
    public class DeviceInfo : IDeviceInfo
    {
        public DeviceInfo(DeviceType type)
        {
            Type = type;
        }

        public DeviceType Type { get; }
        public bool IsRemote { get; set; }
        public IDeviceStatus Status { get; set; }
        public string DeviceId { get; set; }
        public string PNPDeviceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Caption { get; set; }

        public override string ToString()
        {
            return DeviceId;
        }

        public void CopyFrom(IDeviceInfo deviceInfo)
        {
            IsRemote = deviceInfo.IsRemote;
            Status = deviceInfo.Status;
            DeviceId = deviceInfo.DeviceId;
            PNPDeviceId = deviceInfo.PNPDeviceId;
            Name = deviceInfo.Name;
            Description = deviceInfo.Description;
            Caption = deviceInfo.Caption;
        }
    }
}
