using System;
using System.Collections.Generic;
using System.Management;
using SystemMonitor.Application.Interfaces.Devices;
using SystemMonitor.Domain.Interfaces;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Application.Devices
{
    public abstract class DeviceMonitor 
        : IDeviceMonitor
        , ISingletonDependency
    {
        private readonly string _wmiClassName;
        private readonly DeviceWatcher _watcher;

        protected DeviceMonitor(string wmiClassName)
        {
            _wmiClassName = wmiClassName;
            _watcher = new DeviceWatcher(wmiClassName);
        }

        protected abstract IDeviceInfo Map(ManagementBaseObject managementBaseObject);

        public event EventHandler<DeviceMonitorEventArgs> Connected;
        protected virtual void Device_Connected(object sender, EventArrivedEventArgs e)
        {
            var device = GetDeviceFromEvent(e);
            var deviceInfo = Map(device);
            var args = new DeviceMonitorEventArgs(deviceInfo);
            Connected?.Invoke(sender, args);
        }

        public event EventHandler<DeviceMonitorEventArgs> Disconnected;
        protected virtual void Device_Disconnected(object sender, EventArrivedEventArgs e)
        {
            var device = GetDeviceFromEvent(e);
            var deviceInfo = Map(device);
            var args = new DeviceMonitorEventArgs(deviceInfo);
            Disconnected?.Invoke(sender, args);
        }

        private ManagementBaseObject GetDeviceFromEvent(EventArrivedEventArgs e)
        {
            return (ManagementBaseObject)e.NewEvent.GetPropertyValue("TargetInstance");
        }

        public virtual void Start()
        {
            SubscribeForEvents();
            _watcher.Start();
        }

        private void SubscribeForEvents()
        {
            UnsubscribeFromEvents();
            _watcher.Connected += Device_Connected;
            _watcher.Disconnected += Device_Disconnected;
        }

        public virtual void Stop()
        {
            UnsubscribeFromEvents();
        }

        private void UnsubscribeFromEvents()
        {
            _watcher.Connected -= Device_Connected;
            _watcher.Disconnected -= Device_Disconnected;
        }

        public virtual IList<IDeviceInfo> GetDevices()
        {
            var devices = new List<IDeviceInfo>();
            using (var managementDevices = GetManagementDeviceCollection())
            {
                foreach (var managementDevice in managementDevices)
                {
                    devices.Add(Map(managementDevice));
                }
            }
            return devices;
        }

        protected ManagementObjectCollection GetManagementDeviceCollection()
        {
            return GetManagementCollection(_wmiClassName);
        }

        protected ManagementObjectCollection GetManagementCollection(string from)
        {
            using (var searcher = new ManagementObjectSearcher($"SELECT * FROM {from}"))
            {
                return searcher.Get();
            }
        }
    }
}
