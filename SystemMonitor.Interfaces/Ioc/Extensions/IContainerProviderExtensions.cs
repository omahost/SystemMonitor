using Prism.Ioc;
using System.Reflection;

namespace SystemMonitor.Interfaces.Ioc
{
    public static class IContainerProviderExtensions
    {
        public static void RegisterTypesFrom(this IContainerProvider self, Assembly assembly)
        {
            self.Resolve<IIocManager>().RegisterTypes(assembly);
        }

        public static void Initialize(this IContainerProvider self)
        {
            self.Resolve<IIocManager>().InstantiateTypes();
        }
    }
}
