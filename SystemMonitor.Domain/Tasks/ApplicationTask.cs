using System;
using SystemMonitor.Domain.Interfaces.Tasks;

namespace SystemMonitor.Domain.Tasks
{
    public class ApplicationTask : IApplicationTask
    {
        private ApplicationTask(int id, ApplicationTaskType type)
        {
            Id = id;
            Type = type;
        }

        public int Id { get; private set; }
        public string Name => Type.ToString();
        public ApplicationTaskType Type { get; private set; }
        public string DeviceId { get; set; }

        public static ApplicationTask Create(int id, ApplicationTaskType type)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            if (type == ApplicationTaskType.Unknown)
            {
                throw new ArgumentException(nameof(type));
            }
            return new ApplicationTask(id, type);
        }
    }
}
