using System.Management;

namespace SystemMonitor.Application.Devices
{
    public class USBControllerDeviceInfo
    {
        private USBControllerDeviceInfo() 
        {
        }

        public string Dependent { get; private set; }
        public string PNPDeviceId { get; private set; }

        private const string DeviceIdKey = "Win32_PnPEntity.DeviceID=\"";
        public static USBControllerDeviceInfo Create(ManagementBaseObject managementDevice)
        {
            var dependent = managementDevice.GetPropertyString("Dependent");
            var pnpDeviceId = ParsePNPDeviceId(dependent);
            return new USBControllerDeviceInfo
            {
                Dependent = dependent,
                PNPDeviceId = pnpDeviceId
            };
        }

        private static string ParsePNPDeviceId(string dependent)
        {
            var deviceIdStartIndex = dependent.IndexOf(DeviceIdKey) + DeviceIdKey.Length;
            var deviceIdEndIndex = dependent.IndexOf('\"', deviceIdStartIndex);
            var deviceId = dependent
                .Substring(deviceIdStartIndex, deviceIdEndIndex - deviceIdStartIndex)
                .Replace("\\\\", "\\");
            return deviceId;
        }
    }
}
