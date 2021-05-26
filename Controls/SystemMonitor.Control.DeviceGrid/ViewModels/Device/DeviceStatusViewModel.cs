using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Control.DeviceGrid.ViewModels
{
    public class DeviceStatusViewModel
    {
        private readonly IDeviceStatus _deviceStatus;
        public DeviceStatusViewModel(IDeviceStatus deviceStatus)
        {
            _deviceStatus = deviceStatus;
        }

        public DeviceStatusType Type => _deviceStatus.Type;
        public string Description => _deviceStatus.Description;
    }
}
