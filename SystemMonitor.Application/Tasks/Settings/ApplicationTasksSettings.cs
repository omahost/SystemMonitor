using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using SystemMonitor.Domain.Interfaces.Tasks;
using SystemMonitor.Domain.Tasks;

namespace SystemMonitor.Application.Tasks
{
    /// <summary>
    /// This can be replaced by store/get from API
    /// This was done for fast test task implementation
    /// </summary>
    public class ApplicationTasksSettings : IApplicationTasksSettings
    {
        private const string SettingsKeyPrefix = "ApplicationTasks_";
        private Properties.Settings Settings => Properties.Settings.Default;

        public Uri GetEndpointUrl()
        {
            return new Uri(Settings.ApplicationTasks_EndpointUrl);
        }

        public void SetEndpointUrl(Uri url)
        {
            if (!url.IsAbsoluteUri)
            {
                throw new ArgumentException();
            }

            Settings.ApplicationTasks_EndpointUrl = url.AbsoluteUri;
            Save();
        }

        private List<IApplicationTask> _tasks;
        public IList<IApplicationTask> GetTasks()
        {
            if (_tasks == null)
            {
                _tasks = new List<IApplicationTask>
                {
                    Create(1, ApplicationTaskType.Kitchen),
                    Create(2, ApplicationTaskType.Bar),
                };
            }
            return _tasks.ToList();
        }

        private IApplicationTask Create(int id, ApplicationTaskType type)
        {
            var task = ApplicationTask.Create(id, type);
            task.DeviceId = GetTaskDeviceId(task);
            return task;
        }

        public void SetTaskDeviceId(IApplicationTask task, string deviceId)
        {
            var propertyName = GetDeviceIdPropertyName(task);
            GetOrAddProperty<string>(propertyName);
            Settings[propertyName] = deviceId;
            Save();

            task.DeviceId = deviceId;
        }

        public string GetTaskDeviceId(IApplicationTask task)
        {
            var propertyName = GetDeviceIdPropertyName(task);
            var setting = GetProperty(propertyName);
            if (setting == null)
            {
                return null;
            }
            return (string)Settings[propertyName];
        }

        private string GetDeviceIdPropertyName(IApplicationTask task)
        {
            return GetPropertyName(task, nameof(IApplicationTask.DeviceId));
        }

        private string GetPropertyName(IApplicationTask task, string propertyName)
        {
            return $"{GetPropertyPrefix(task)}{propertyName}";
        }

        private string GetPropertyPrefix(IApplicationTask task)
        {
            return $"{SettingsKeyPrefix}{task.Id}_";
        }

        private SettingsProperty GetOrAddProperty<T>(string propertyName)
        {
            var setting = GetProperty(propertyName);
            if (setting == null)
            {
                setting = CreateProperty<T>(propertyName);
            }
            return setting;
        }

        private SettingsProperty CreateProperty<T>(string propertyName)
        {
            var setting = new SettingsProperty(propertyName)
            {
                PropertyType = typeof(T),
                DefaultValue = default(T),
                Provider = Settings.Providers["LocalFileSettingsProvider"],
            };

            setting.Attributes.Add(
                typeof(UserScopedSettingAttribute),
                new UserScopedSettingAttribute()
                );

            setting.Attributes.Add(
                typeof(DefaultSettingValueAttribute),
                new DefaultSettingValueAttribute("")
                );

            Settings.Properties.Add(setting);
            Settings.Reload();
            return setting;
        }

        private SettingsProperty GetProperty(string propertyName)
        {
            return Settings.Properties[propertyName];
        }

        private void Save()
        {
            Settings.Save();
        }
    }
}
