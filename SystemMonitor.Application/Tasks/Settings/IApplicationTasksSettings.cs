using System;
using System.Collections.Generic;
using SystemMonitor.Domain.Interfaces.Tasks;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Application.Tasks
{
    public interface IApplicationTasksSettings : ISingletonDependency
    {
        Uri GetEndpointUrl();
        void SetEndpointUrl(Uri url);
        IList<IApplicationTask> GetTasks();
        void SetTaskDeviceId(IApplicationTask task, string deviceId);
    }
}