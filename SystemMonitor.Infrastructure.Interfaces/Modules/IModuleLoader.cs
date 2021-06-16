using SystemMonitor.Interfaces.Ioc;
using Prism.Modularity;

namespace SystemMonitor.Infrastructure.Interfaces.Modules
{
    public interface IModuleLoader : ISingletonDependency
    {
        void LoadModules(IModuleCatalog catalog);
    }
}
