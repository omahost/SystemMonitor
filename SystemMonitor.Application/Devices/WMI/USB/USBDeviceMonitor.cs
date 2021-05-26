using System.Collections.Generic;
using System.Linq;
using System.Management;
using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Application.Devices
{
    public class USBDeviceMonitor 
        : DeviceMonitor
        , IUSBDeviceMonitor
    {
        private readonly IPnPDeviceMonitor _pnpDeviceMonitor;

        // Win32_PnPEntity does include all the USB devices, and hundreds more non-USB devices
        // If use a WHERE clause search Win32_PnPEntity for a DeviceID beginning with "USB%" to filter the list is helpful 
        // but slightly incomplete; it misses bluetooth devices, some printers/print servers, and HID-compliant mice and keyboards. 
        // I have seen "USB\%", "USBSTOR\%", "USBPRINT\%", "BTH\%", "SWD\%", and "HID\%"
        // best way to enumerate USB devices was to query Win32_USBControllerDevice
        public USBDeviceMonitor(
            IPnPDeviceMonitor pnPDeviceMonitor
            )
            : base("Win32_USBControllerDevice")
        {
            _pnpDeviceMonitor = pnPDeviceMonitor;
        }
        
        public override IList<IDeviceInfo> GetDevices()
        {
            InitializePNPDevices();
            return base.GetDevices();
        }

        private IList<IDeviceInfo> _pnpDevices;
        private void InitializePNPDevices()
        {
            _pnpDevices = _pnpDeviceMonitor.GetDevices();
        }

        protected override IDeviceInfo Map(ManagementBaseObject managementDevice)
        {
            var controllerDevice = USBControllerDeviceInfo.Create(managementDevice);
            var pnpDevice = _pnpDevices.FirstOrDefault(item => item.PNPDeviceId == controllerDevice.PNPDeviceId);
            if (pnpDevice == null)
            {
                throw new KeyNotFoundException($"PNP Device not found for PNPDeviceId={controllerDevice.PNPDeviceId}");
            }
            return new USBDeviceInfo(pnpDevice);
        }
    }
}
