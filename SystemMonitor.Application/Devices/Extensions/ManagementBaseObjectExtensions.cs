using System;
using System.Management;
using System.Text;

namespace SystemMonitor.Application.Devices
{
    public static class ManagementBaseObjectExtensions
    {
        public static string GetPropertyString(this ManagementBaseObject self, string propertyName)
        {
            return self.GetPropertyValue(propertyName)?.ToString();
        }

        public static bool GetPropertyBool(this ManagementBaseObject self, string propertyName)
        {
            return bool.Parse(self.GetPropertyString(propertyName));
        }

        public static int GetPropertyInt(this ManagementBaseObject self, string propertyName)
        {
            return int.Parse(self.GetPropertyString(propertyName));
        }

        public static T GetPropertyEnum<T>(this ManagementBaseObject self, string propertyName)
        {
            return (T)Enum.Parse(typeof(T), self.GetPropertyString(propertyName));
        }

        public static string ToNameValueString(this PropertyDataCollection self) 
        {
            var builder = new StringBuilder();
            foreach (var property in self)
            {
                builder.AppendLine($"{property.Name}={property.Value}");
            }
            return builder.ToString();
        }
    }
}
