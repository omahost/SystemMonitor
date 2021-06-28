using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemMonitor.Application.Interfaces.Tasks;
using SystemMonitor.Application.Interfaces.Tasks.Events;
using SystemMonitor.Domain.Interfaces;
using SystemMonitor.Domain.Interfaces.Tasks;

namespace SystemMonitor.Application.Tasks
{
    public class ApplicationTaskFacade : IApplicationTaskFacade
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IApplicationTasksSettings _applicationTasksSettings;

        public ApplicationTaskFacade(
            IEventAggregator eventAggregator,
            IApplicationTasksSettings applicationTasksSettings
            )
        {
            _eventAggregator = eventAggregator;
            _applicationTasksSettings = applicationTasksSettings;
        }

        public Uri GetEndpointUrl()
        {
            return _applicationTasksSettings.GetEndpointUrl();
        }

        public void SetEndpointUrl(Uri url)
        {
            _applicationTasksSettings.SetEndpointUrl(url);
            TriggerEndpointUrlChanged(url);
        }

        private void TriggerEndpointUrlChanged(Uri url)
        {
            _eventAggregator
                .GetEvent<ApplicationTasksEndpointUrlChanged>()
                .Publish(new ApplicationTasksEndpointUrlChangedEventArgs(url));
        }

        public IList<IApplicationTask> GetTasks()
        {
            return _applicationTasksSettings.GetTasks();
        }

        public IApplicationTask GetTask(ApplicationTaskType type)
        {
            return GetTasks().First(task => task.Type == type);
        }

        public void SetTaskDevice(IApplicationTask task, IDeviceInfo deviceInfo)
        {
            var previousDeviceId = task.DeviceId;
            _applicationTasksSettings.SetTaskDeviceId(task, deviceInfo?.DeviceId);
            TriggerTaskDeviceChanged(task, previousDeviceId);
        }

        private void TriggerTaskDeviceChanged(IApplicationTask task, string previousDeviceId)
        {
            _eventAggregator
                .GetEvent<ApplicationTaskDeviceChanged>()
                .Publish(new ApplicationTaskDeviceChangedEventArgs(task, previousDeviceId));
        }
    }
}
