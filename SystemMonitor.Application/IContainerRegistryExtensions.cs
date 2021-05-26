using Prism.Ioc;
using System.Reflection;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.Application
{
    public static class IContainerRegistryExtensions
    {
        public static IContainerProvider AddApplication(this IContainerProvider self)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            self.RegisterTypesFrom(executingAssembly);

            return self;
        }
    }
}
