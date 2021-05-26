using System.Reflection;
using Prism.Ioc;
using SystemMonitor.Interfaces.Ioc;
using SystemMonitor.Infrastructure.Ioc;

namespace SystemMonitor.Infrastructure
{
    public static class IContainerRegistryExtensions
    {
        public static IContainerProvider AddInfrastructure(this IContainerProvider self, IContainerRegistry containerRegistry)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            containerRegistry.RegisterSingleton<IIocManager, IocManager>();

            self.RegisterTypesFrom(executingAssembly);
            return self;
        }
    }
}
