using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Control.DeviceGrid.ViewModels
{
    public class DeviceInfoViewModel
    {
        private readonly IDeviceInfo _deviceInfo;
        private readonly DeviceStatusViewModel _deviceStatus;
        public DeviceInfoViewModel(IDeviceInfo deviceInfo)
        {
            _deviceInfo = deviceInfo;
            _deviceStatus = new DeviceStatusViewModel(deviceInfo.Status);
        }

        internal IDeviceInfo DeviceInfo => _deviceInfo;

        public DeviceType Type => _deviceInfo.Type;
        public bool IsRemote => _deviceInfo.IsRemote;

        public DeviceStatusType StatusType => _deviceStatus.Type;
        public string StatusInfo => _deviceStatus.Description;

        public string DeviceId => _deviceInfo.DeviceId;
        public string PNPDeviceId => _deviceInfo.PNPDeviceId;
        public string Name => _deviceInfo.Name;
        public string Description => _deviceInfo.Description;
        public string Caption => _deviceInfo.Caption;
    }
}
