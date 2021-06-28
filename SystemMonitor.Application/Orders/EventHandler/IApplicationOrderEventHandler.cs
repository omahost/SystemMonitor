using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Application.Orders
{
    public interface IApplicationOrderEventHandler
        : ISingletonDependency
        , IInstantiateDependency
    {
    }
}
