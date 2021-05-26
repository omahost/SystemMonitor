using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Application.Interfaces.App
{
    public interface IApplicationStartup 
        : ISingletonDependency
    {
        void Run();
    }
}
