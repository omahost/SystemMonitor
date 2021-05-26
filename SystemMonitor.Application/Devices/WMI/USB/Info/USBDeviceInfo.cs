using SystemMonitor.Domain.Devices;
using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Application.Devices
{
    public class USBDeviceInfo : DeviceInfo
    {
        private USBDeviceInfo()
            : base(DeviceType.USB)
        {
        }

        public USBDeviceInfo(IDeviceInfo deviceInfo)
            : this()
        {
            CopyFrom(deviceInfo);
        }
    }
}
