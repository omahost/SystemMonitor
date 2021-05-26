using System;
using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Application.Interfaces.Devices
{
    public class DeviceMonitorEventArgs : EventArgs
    {
        public IDeviceInfo Device { get; }

        public DeviceMonitorEventArgs(IDeviceInfo device)
        {
            Device = device;
        }
    }
}
