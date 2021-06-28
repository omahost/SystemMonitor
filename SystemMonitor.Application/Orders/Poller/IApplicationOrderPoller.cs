using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Application.Orders
{
    public interface IApplicationOrderPoller
        : ISingletonDependency
        , IInstantiateDependency
        , IInitializeDependency
    {
    }
}
