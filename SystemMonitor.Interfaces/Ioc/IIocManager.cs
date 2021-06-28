using System.Reflection;

namespace SystemMonitor.Interfaces.Ioc
{
    public interface IIocManager : ISingletonDependency
    {
        void RegisterTypes(Assembly assembly);
        void InstantiateTypes();
    }
}
