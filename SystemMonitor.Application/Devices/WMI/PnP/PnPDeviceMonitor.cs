using System.Management;
using SystemMonitor.Domain.Devices;
using SystemMonitor.Domain.Interfaces;

namespace SystemMonitor.Application.Devices
{
    public class PnPDeviceMonitor 
        : DeviceMonitor
        , IPnPDeviceMonitor
    {
        private readonly IPnPDeviceStatusFactory _pnpDeviceStatusFactory;

        public PnPDeviceMonitor(
            IPnPDeviceStatusFactory pnpDeviceStatusFactory
            )
            : base("Win32_PnPEntity")
        {
            _pnpDeviceStatusFactory = pnpDeviceStatusFactory;
        }

        protected override IDeviceInfo Map(ManagementBaseObject managementDevice)
        {
            return new DeviceInfo(DeviceType.PnP)
            {
                DeviceId = managementDevice.GetPropertyString("DeviceID"),
                PNPDeviceId = managementDevice.GetPropertyString("PNPDeviceID"),
                Description = managementDevice.GetPropertyString("Description"),
                Name = managementDevice.GetPropertyString("Name"),
                Caption = managementDevice.GetPropertyString("Caption"),
                Status = _pnpDeviceStatusFactory.Create(managementDevice),
            };
        }
    }
}
