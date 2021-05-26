using System;
using System.Collections.Generic;
using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Application.Interfaces.Devices
{
    public interface IDeviceMonitor
    {
        void Start();
        void Stop();

        IList<IDeviceInfo> GetDevices();

        event EventHandler<DeviceMonitorEventArgs> Connected;
        event EventHandler<DeviceMonitorEventArgs> Disconnected;
    }
}
