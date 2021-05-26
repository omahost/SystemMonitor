using System;
using System.Collections.Generic;
using System.Linq;
using SystemMonitor.Application.Interfaces.Devices;
using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Application.Devices
{
    public class DeviceMonitorFacade : IDeviceMonitorFacade
    {
        private readonly IDeviceMonitor[] _monitors;

        public DeviceMonitorFacade(
            IUSBDeviceMonitor usbDeviceMonitor,
            IPrinterDeviceMonitor printerDeviceMonitor
            )
        {
            _monitors = new IDeviceMonitor[]
            {
                usbDeviceMonitor,
                printerDeviceMonitor
                // TODO: add other monitors here
            };
        }

        public event EventHandler<DeviceMonitorEventArgs> Connected;
        private void Device_Connected(object sender, DeviceMonitorEventArgs e)
        {
            Connected?.Invoke(sender, e);
        }

        public event EventHandler<DeviceMonitorEventArgs> Disconnected;
        private void Device_Disconnected(object sender, DeviceMonitorEventArgs e)
        {
            Disconnected?.Invoke(sender, e);
        }

        public void Start()
        {
            ForEachMonitor(monitor => 
            {
                SubscribeForEvents(monitor);
                monitor.Start();
            });
        }

        private void SubscribeForEvents(IDeviceMonitor monitor)
        {
            UnsubscribeFromEvents(monitor);
            monitor.Connected += Device_Connected;
            monitor.Disconnected += Device_Disconnected;
        }

        public void Stop()
        {
            ForEachMonitor(monitor => 
            {
                UnsubscribeFromEvents(monitor);
                monitor.Stop();
            });
        }

        private void UnsubscribeFromEvents(IDeviceMonitor monitor)
        {
            monitor.Connected -= Device_Connected;
            monitor.Disconnected -= Device_Disconnected;
        }

        private void ForEachMonitor(Action<IDeviceMonitor> action)
        {
            foreach (var monitor in _monitors)
            {
                action(monitor);
            }
        }

        public IList<IDeviceInfo> GetDevices()
        {
            var devices = _monitors
                .SelectMany(monitor => monitor.GetDevices())
                .ToList();
            return devices;
        }
    }
}
