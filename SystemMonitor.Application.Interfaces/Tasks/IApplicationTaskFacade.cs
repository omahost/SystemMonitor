using System;
using System.Collections.Generic;
using SystemMonitor.Domain.Interfaces;
using SystemMonitor.Domain.Interfaces.Tasks;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Application.Interfaces.Tasks
{
    public interface IApplicationTaskFacade
       : ISingletonDependency
    {
        Uri GetEndpointUrl();
        void SetEndpointUrl(Uri url);

        IList<IApplicationTask> GetTasks();
        IApplicationTask GetTask(ApplicationTaskType type);
        void SetTaskDevice(IApplicationTask task, IDeviceInfo deviceInfo);
    }
}
