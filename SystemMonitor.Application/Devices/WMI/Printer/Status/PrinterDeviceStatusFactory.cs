using System.Management;
using SystemMonitor.Domain.Devices;
using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Application.Devices
{
    public class PrinterDeviceStatusFactory : IPrinterDeviceStatusFactory
    {
        public IDeviceStatus Create(ManagementBaseObject managementDevice)
        {
            var status = managementDevice.GetPropertyEnum<ExtendedPrinterStatusType>("ExtendedPrinterStatus");
            var statusType = GetStatusType(status);
            var description = managementDevice.GetPropertyString("ErrorDescription");
            if (string.IsNullOrEmpty(description))
            {
                description = managementDevice.GetPropertyString("StatusInfo");
            }
            return new DeviceStatus(statusType, description);
        }

        private DeviceStatusType GetStatusType(ExtendedPrinterStatusType value)
        {
            if (value == ExtendedPrinterStatusType.Other)
            {
                return DeviceStatusType.Other;
            }

            if (value == ExtendedPrinterStatusType.Undefined
                || value == ExtendedPrinterStatusType.Unknown)
            {
                return DeviceStatusType.Unknown;
            }

            if (value == ExtendedPrinterStatusType.Offline
                || value == ExtendedPrinterStatusType.NotAvailable)
            {
                return DeviceStatusType.Off;
            }

            if (value == ExtendedPrinterStatusType.Error)
            {
                return DeviceStatusType.Error;
            }

            if (value == ExtendedPrinterStatusType.Idle
                || value == ExtendedPrinterStatusType.Initialization)
            {
                return DeviceStatusType.On;
            }

            if (value == ExtendedPrinterStatusType.Busy
                || value == ExtendedPrinterStatusType.Printing
                || value == ExtendedPrinterStatusType.Processing
                || value == ExtendedPrinterStatusType.WarmingUp
                || value == ExtendedPrinterStatusType.Waiting)
            {
                return DeviceStatusType.AtWork;
            }
            
            return DeviceStatusType.Other;
        }
    }
}
