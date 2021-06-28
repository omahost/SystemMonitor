using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SystemMonitor.Application.Interfaces.Devices;
using SystemMonitor.Application.Interfaces.Tasks;
using SystemMonitor.Common.Extensions;
using SystemMonitor.Control.Dialogs;
using SystemMonitor.Control.Extensions;
using SystemMonitor.Control.Settings.PinCode;
using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Control.Settings
{
    public class SettingsViewModel : DialogViewModel
    {
        private readonly IDeviceMonitorFacade _deviceMonitorFacade;
        private readonly IApplicationTaskFacade _applicationTaskFacade;
        private readonly ISettingsPinCodeFacade _settingsPinCodeFacade;

        public SettingsViewModel(
            IDeviceMonitorFacade deviceMonitorFacade,
            IApplicationTaskFacade applicationTaskFacade,
            ISettingsPinCodeFacade settingsPinCodeFacade
            )
        {
            _deviceMonitorFacade = deviceMonitorFacade;
            _applicationTaskFacade = applicationTaskFacade;
            _settingsPinCodeFacade = settingsPinCodeFacade;

            ChangeTasksEndpointCommand = new DelegateCommand(
                ChangeTasksEndpoint, 
                CanChangeTasksEndpoint
                );

            SaveTasksEndpointCommand = new DelegateCommand(
                SaveTasksEndpoint,
                CanSaveTasksEndpoint
                );

            InitializeAsync();
        }

        private void InitializeAsync()
        {
            Task.Run(Initialize);
        }

        private void Initialize()
        {
            InitializeTasksEndpoint();
            InitializeDevices();
            InitializeTasks();
        }

        private void InitializeTasksEndpoint()
        {
            var endpointUrl = _applicationTaskFacade
                .GetEndpointUrl()
                ?.AbsoluteUri;

            this.InvokeOnUiThread(() => TasksEndpoint = endpointUrl);
        }

        private IList<DeviceViewModel> _devices;
        private void InitializeDevices()
        {
            _devices = _deviceMonitorFacade
                .GetDevices()
                .Where(device => device.Type == DeviceType.Printer)
                .Select(device => new DeviceViewModel(device))
                .ToList();

            // Empty one
            _devices.Insert(0, DeviceViewModel.Empty);
        }

        private void InitializeTasks()
        {
            var tasks = _applicationTaskFacade
                .GetTasks()
                .Select(task => new ApplicationTaskViewModel(_applicationTaskFacade, task, _devices))
                .ToObservableCollection()
                ;

            this.InvokeOnUiThread(() => Tasks = tasks);
        }

        private ObservableCollection<ApplicationTaskViewModel> _tasks;
        public ObservableCollection<ApplicationTaskViewModel> Tasks
        {
            get => _tasks;
            private set => SetProperty(ref _tasks, value);
        }

        private string _tasksEndpoint;
        public string TasksEndpoint
        {
            get => _tasksEndpoint;
            set
            {
                if (SetProperty(ref _tasksEndpoint, value))
                {
                    RevalidateEndpointCommands();
                }
            }
        }

        private bool _isTasksEndpointReadonly = true;
        public bool IsTasksEndpointReadonly
        {
            get => _isTasksEndpointReadonly;
            set 
            {
                if (SetProperty(ref _isTasksEndpointReadonly, value))
                {
                    RevalidateEndpointCommands();
                }
            }
        }

        private void RevalidateEndpointCommands()
        {
            SaveTasksEndpointCommand.RaiseCanExecuteChanged();
            ChangeTasksEndpointCommand.RaiseCanExecuteChanged();
        }

        public DelegateCommand ChangeTasksEndpointCommand { get; }
        private void ChangeTasksEndpoint()
        {
            if (_settingsPinCodeFacade.ValidatePinCode())
            {
                IsTasksEndpointReadonly = false;
            }
        }

        private bool CanChangeTasksEndpoint()
        {
            return IsTasksEndpointReadonly;
        }

        public DelegateCommand SaveTasksEndpointCommand { get; }
        private void SaveTasksEndpoint()
        {
            _applicationTaskFacade.SetEndpointUrl(new Uri(TasksEndpoint));
            IsTasksEndpointReadonly = true;
        }

        private bool CanSaveTasksEndpoint()
        {
            if (IsTasksEndpointReadonly)
            {
                return false;
            }

            if (!Uri.TryCreate(TasksEndpoint, UriKind.Absolute, out var uri))
            {
                return false;
            }

            return uri.IsAbsoluteUri;
        }
    }
}
