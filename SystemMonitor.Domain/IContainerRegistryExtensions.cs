using Prism.Ioc;
using System.Reflection;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Domain
{
    public static class IContainerRegistryExtensions
    {
        public static IContainerProvider AddDomain(this IContainerProvider self)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            self.RegisterTypesFrom(executingAssembly);

            return self;
        }
    }
}
