using System.Management;
using System.Text;
using SystemMonitor.Domain.Devices;
using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Application.Devices
{
    public class PrinterDeviceMonitor 
        : DeviceMonitor
        , IPrinterDeviceMonitor
    {
        private readonly IPrinterDeviceStatusFactory _printerDeviceStatusFactory;

        public PrinterDeviceMonitor(
            IPrinterDeviceStatusFactory printerDeviceStatusFactory
            )
            : base("Win32_Printer")
        {
            _printerDeviceStatusFactory = printerDeviceStatusFactory;
        }

        protected override IDeviceInfo Map(ManagementBaseObject managementDevice)
        {
            return new DeviceInfo(DeviceType.Printer)
            {
                DeviceId = managementDevice.GetPropertyString("DeviceID"),
                PNPDeviceId = managementDevice.GetPropertyString("PNPDeviceID"),
                Description = managementDevice.GetPropertyString("Description"),
                Name = managementDevice.GetPropertyString("Name"),
                Caption = managementDevice.GetPropertyString("Caption"),
                Status = _printerDeviceStatusFactory.Create(managementDevice),
                IsRemote = !managementDevice.GetPropertyBool("Local")
            };
        }
    }
}
