using System;

namespace SystemMonitor.Application.Interfaces.Tasks.Events
{
    public class ApplicationTasksEndpointUrlChangedEventArgs : EventArgs
    {
        public Uri EndpointUrl { get; }

        public ApplicationTasksEndpointUrlChangedEventArgs(Uri endpointUrl)
        {
            EndpointUrl = endpointUrl;
        }
    }
}
