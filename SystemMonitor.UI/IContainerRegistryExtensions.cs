using Prism.Ioc;
using System.Reflection;
using SystemMonitor.Interfaces.Ioc;

namespace SystemMonitor.UI
{
    public static class IContainerRegistryExtensions
    {
        public static IContainerProvider AddUI(this IContainerProvider self)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            self.RegisterTypesFrom(executingAssembly);

            return self;
        }
    }
}
