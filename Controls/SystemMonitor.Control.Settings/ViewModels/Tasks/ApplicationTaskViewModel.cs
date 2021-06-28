using System.Collections.Generic;
using System.Linq;
using SystemMonitor.Application.Interfaces.Tasks;
using SystemMonitor.Control.Views;
using SystemMonitor.Domain.Interfaces.Tasks;

namespace SystemMonitor.Control.Settings
{
    public class ApplicationTaskViewModel : ViewModelBase
    {
        private readonly IApplicationTask _applicationTask;
        private readonly ICollection<DeviceViewModel> _devices;
        private readonly IApplicationTaskFacade _applicationTaskFacade;

        public ApplicationTaskViewModel(
            IApplicationTaskFacade applicationTaskFacade,
            IApplicationTask applicationTask,
            ICollection<DeviceViewModel> devices
            )
        {
            _devices = devices;
            _applicationTask = applicationTask;
            _applicationTaskFacade = applicationTaskFacade;

            InitializeSelectedDevice();
        }

        private void InitializeSelectedDevice()
        {
            var deviceId = _applicationTask.DeviceId;
            if (string.IsNullOrEmpty(deviceId))
            {
                return;
            }

            _selectedDevice = _devices.FirstOrDefault(
                device => device.Id == deviceId);
        }

        public int Id => _applicationTask.Id;
        public string Name => _applicationTask.Name;
        public ICollection<DeviceViewModel> Devices => _devices;

        private DeviceViewModel _selectedDevice;
        public DeviceViewModel SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                if (SetProperty(ref _selectedDevice, value))
                {
                    _applicationTaskFacade.SetTaskDevice(_applicationTask, value?.DeviceInfo);
                }
            }
        }
    }
}
