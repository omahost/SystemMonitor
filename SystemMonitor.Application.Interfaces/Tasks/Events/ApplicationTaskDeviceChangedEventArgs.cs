using System;
using SystemMonitor.Domain.Interfaces.Tasks;

namespace SystemMonitor.Application.Interfaces.Tasks.Events
{
    public class ApplicationTaskDeviceChangedEventArgs : EventArgs
    {
        public IApplicationTask Task { get; }
        public string PreviousDeviceId { get; }
        public bool HasPreviousDeviceId => !string.IsNullOrEmpty(PreviousDeviceId);

        public ApplicationTaskDeviceChangedEventArgs(IApplicationTask task, string previousDeviceId)
        {
            Task = task;
            PreviousDeviceId = previousDeviceId;
        }
    }
}
