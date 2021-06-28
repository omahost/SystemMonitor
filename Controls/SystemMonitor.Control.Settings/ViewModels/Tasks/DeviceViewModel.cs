using SystemMonitor.Control.Views;
using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Control.Settings
{
    public class DeviceViewModel : ViewModelBase
    {
        public static readonly  DeviceViewModel Empty = new DeviceViewModel();

        public DeviceViewModel(
            IDeviceInfo deviceInfo = null
            )
        {
            DeviceInfo = deviceInfo;
        }

        public IDeviceInfo DeviceInfo { get; }
        public string Id => DeviceInfo?.DeviceId;
        public string Name => DeviceInfo?.Name;
    }
}
