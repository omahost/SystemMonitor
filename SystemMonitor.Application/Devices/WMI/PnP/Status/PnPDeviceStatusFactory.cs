using System.Management;
using SystemMonitor.Domain.Devices;
using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Application.Devices
{
    public class PnPDeviceStatusFactory : IPnPDeviceStatusFactory
    {
        public IDeviceStatus Create(ManagementBaseObject managementDevice)
        {
            var status = managementDevice.GetPropertyString("Status");
            var statusType = GetStatusType(status);
            var description = managementDevice.GetPropertyString("ErrorDescription");
            if (string.IsNullOrEmpty(description))
            {
                description = managementDevice.GetPropertyString("StatusInfo");
            }
            return new DeviceStatus(statusType, description);
        }

        private DeviceStatusType GetStatusType(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return DeviceStatusType.Unknown;
            }

            if (value == "OK")
            {
                return DeviceStatusType.On;
            }

            return DeviceStatusType.Other;
        }
    }
}
