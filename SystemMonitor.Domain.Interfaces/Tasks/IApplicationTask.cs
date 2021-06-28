namespace SystemMonitor.Domain.Interfaces.Tasks
{
    public interface IApplicationTask
    {
        int Id { get; }
        string Name { get; }
        ApplicationTaskType Type { get; }
        string DeviceId { get; set; }
    }
}
