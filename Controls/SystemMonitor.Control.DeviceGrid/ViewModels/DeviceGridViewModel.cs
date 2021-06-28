using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SystemMonitor.Application.Interfaces.Devices;
using SystemMonitor.Control.DeviceGrid.ViewModels;
using SystemMonitor.Control.Extensions;
using SystemMonitor.Control.Receipt.Interfaces;
using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Control.DeviceGrid
{
    public class DeviceGridViewModel : BindableBase
    {
        private readonly IDeviceMonitorFacade _deviceMonitorFacade;
        private readonly IReceiptPrinterFacade _receiptPrinterFacade;

        public DeviceGridViewModel(
            IDeviceMonitorFacade deviceMonitorFacade,
            IReceiptPrinterFacade receiptPrinterFacade
            )
        {
            _deviceMonitorFacade = deviceMonitorFacade;
            _receiptPrinterFacade = receiptPrinterFacade;
            _deviceMonitorFacade.Connected += Device_Connected;
            _deviceMonitorFacade.Disconnected += Device_Disconnected;

            DoubleClickCommand = new DelegateCommand(OnDoubleClick);

            IntializeDevicesAsync();
        }

        private DeviceInfoViewModel _selectedDevice;
        public DeviceInfoViewModel SelectedDevice
        {
            get => _selectedDevice;
            set => SetProperty(ref _selectedDevice, value);
        }

        public DelegateCommand DoubleClickCommand { get; }
        private void OnDoubleClick()
        {
            var deviceInfo = SelectedDevice.DeviceInfo;
            if (deviceInfo.Type == DeviceType.Printer)
            {
                // TODO: may use CQRS (e.g MediatR), but for test task this should be enough
            }
        }

        private void Device_Connected(object sender, DeviceMonitorEventArgs e)
        {
            var viewModel = CreateDeviceViewModel(e.Device);
            this.InvokeOnUiThread(() => Devices?.Add(viewModel));
        }

        private void Device_Disconnected(object sender, DeviceMonitorEventArgs e)
        {
            var deviceId = e.Device.DeviceId;

            var existingDevice = Devices
                .Where(item => item.DeviceId == deviceId)
                .FirstOrDefault();
            if (existingDevice == null)
            {
                return;
            }

            this.InvokeOnUiThread(() => Devices.Remove(existingDevice));
        }

        private DeviceInfoViewModel CreateDeviceViewModel(IDeviceInfo deviceInfo)
        {
            return new DeviceInfoViewModel(deviceInfo);
        }

        private void IntializeDevicesAsync()
        {
            Task.Run(IntializeDevices);
        }

        private void IntializeDevices()
        {
            var viewModels = _deviceMonitorFacade
                .GetDevices()
                .Select(CreateDeviceViewModel);

            var devices = new ObservableCollection<DeviceInfoViewModel>(viewModels);

            this.InvokeOnUiThread(() => Devices = devices);
        }

        private ObservableCollection<DeviceInfoViewModel> _devices;
        public ObservableCollection<DeviceInfoViewModel> Devices 
        { 
            get => _devices;
            private set => SetProperty(ref _devices, value);
        }
    }
}
