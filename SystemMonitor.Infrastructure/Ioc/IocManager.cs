using SystemMonitor.Interfaces.Ioc;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;
using Prism.Unity;
using SystemMonitor.Infrastructure.Interfaces.Dialogs;

namespace SystemMonitor.Infrastructure.Ioc
{
    public class IocManager : IIocManager
    {
        private readonly IUnityContainer _container;
        private readonly IContainerRegistry _containerRegistry;
        private readonly List<Assembly> _assemblies = new List<Assembly>();
        private readonly List<Type> _typesToInstantiate = new List<Type>();

        public IocManager(
            IContainerRegistry containerRegistry
            )
        {
            _containerRegistry = containerRegistry;
            _container = _containerRegistry.GetContainer();
        }

        public void InstantiateTypes()
        {
            if (_typesToInstantiate.Count == 0)
            {
                return;
            }

            foreach (var type in _typesToInstantiate)
            {
                var interfaceToInstantiate = GetTypeToInstantiate(type);
                if (interfaceToInstantiate == null)
                {
                    throw new NotImplementedException();
                }

                var instance = _container.Resolve(interfaceToInstantiate);
                if (instance is IInitializeDependency dependency)
                {
                    dependency.Initialize();
                }
            }
        }

        private Type GetTypeToInstantiate(Type type)
        {
            return type.GetInterfaces().FirstOrDefault(
                interfaceType => interfaceType.Name == "I" + type.Name
                );
        }

        public void RegisterTypes(Assembly assembly)
        {
            if (IsAssemblyTypesRegisted(assembly))
            {
                return;
            }

            RegisterAssemblyTypes(assembly);
        }

        private bool IsAssemblyTypesRegisted(Assembly assembly)
        {
            return _assemblies.Contains(assembly);
        }

        private void RegisterAssemblyTypes(Assembly assembly)
        {
            _assemblies.Add(assembly);

            RegisterTypes(
                GetTypesToRegister(assembly)
                );
        }

        private Type[] GetTypesToRegister(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(IsTypeToRegister)
                .ToArray();
        }

        private bool IsTypeToRegister(Type type)
        {
            if (!type.IsClass)
            {
                return false;
            }

            if (typeof(ISingletonDependency).IsAssignableFrom(type))
            {
                return true;
            }

            if (typeof(ITransientDependency).IsAssignableFrom(type))
            {
                return true;
            }

            return false;
        }

        private void RegisterTypes(Type[] types)
        {
            if (types.Length == 0)
            {
                return;
            }

            RegisterSingletons(types);
            RegisterTransients(types);
            RegisterScoped(types);
            RegisterDialogs(types);
            RegisterInstantiates(types);
        }

        private void RegisterSingletons(Type[] types)
        {
            RegisterTypes<ISingletonDependency>(types, RegisterSingleton);
        }

        private void RegisterSingleton(Type from, Type to)
        {
            _containerRegistry.RegisterSingleton(from, to);
        }

        private void RegisterTransients(Type[] types)
        {
            RegisterTypes<ITransientDependency>(types, RegisterTransient);
        }

        private void RegisterTransient(Type from, Type to)
        {
            _containerRegistry.Register(from, to);
        }

        private void RegisterScoped(Type[] types)
        {
            RegisterTypes<IScopedDependency>(types, RegisterScoped);
        }

        private void RegisterScoped(Type from, Type to)
        {
            _containerRegistry.RegisterScoped(from, to);
        }

        private void RegisterDialogs(Type[] types)
        {
            var typesToRegister = GetTypes<IDialogView>(types);
            foreach (var typeToRegister in typesToRegister)
            {
                RegisterDialog(typeToRegister);
            }
        }

        private void RegisterDialog(Type typeToRegister)
        {
            var objectType = typeof(object);
            var interfaceTypes = GetInterfaceTypesToRegister(typeToRegister);
            foreach (var interfaceType in interfaceTypes)
            {
                var dependencyName = interfaceType.GetDependencyName();
                // https://github.com/PrismLibrary/Prism/blob/master/src/Wpf/Prism.Wpf/Services/Dialogs/DialogService.cs
                if (_containerRegistry.IsRegistered(objectType, dependencyName))
                {
                    continue;
                }

                // NOTE: that when using factory, we can not resolve instance with arguments
                Func<IUnityContainer, object> factory
                    = c => c.Resolve(interfaceType);

                _container.RegisterFactory(objectType, dependencyName, factory);
            }
        }

        private void RegisterInstantiates(Type[] types)
        {
            _typesToInstantiate.AddRange(
                GetTypes<IInstantiateDependency>(types)
                );
        }

        private void RegisterTypes<TInterface>(Type[] types, Action<Type, Type> register)
        {
            var typesToRegister = GetTypes<TInterface>(types);
            foreach (var typeToRegister in typesToRegister)
            {
                RegisterType(typeToRegister, register);
            }
        }

        private void RegisterType(Type typeToRegister, Action<Type, Type> register)
        {
            var interfaceTypes = GetInterfaceTypesToRegister(typeToRegister);
            foreach (var interfaceType in interfaceTypes)
            {
                if (_containerRegistry.IsRegistered(interfaceType))
                {
                    continue;
                }

                register(interfaceType, typeToRegister);
            }
        }

        private Type[] GetTypes<TInterface>(Type[] types)
        {
            return types
                .Where(type => typeof(TInterface).IsAssignableFrom(type))
                .Where(type => !_containerRegistry.IsRegistered(type))
                .ToArray();
        }

        private Type[] GetInterfaceTypesToRegister(Type self)
        {
            return self.GetInterfaces()
                .Where(IsInterfaceTypeToRegister)
                .ToArray();
        }

        private bool IsInterfaceTypeToRegister(Type type)
        {
            if (IsInterfaceMarker(type))
            {
                return false;
            }

            if (IsInterfaceTypeToIgnore(type))
            {
                return false;
            }

            return true;
        }

        private bool IsInterfaceMarker(Type type)
        {
            return typeof(ISingletonDependency) == type
                || typeof(ITransientDependency) == type
                || typeof(IScopedDependency) == type
                || typeof(IDialogView) == type;
        }

        private List<string> TypeNamePrefixToIgnore = new List<string>
        {
            "System",
            "Micosoft",
            "Windows",
            "Prism",
            "Presentation",
            "netstandard",
            "mscorlib",
            "Unity"
        };

        private bool IsInterfaceTypeToIgnore(Type type)
        {
            return TypeNamePrefixToIgnore.Any(prefix =>
            {
                if (string.IsNullOrEmpty(type.FullName))
                {
                    return false;
                }

                var typeNamespace = prefix + ".";
                return type.FullName.StartsWith(typeNamespace, 
                    StringComparison.InvariantCultureIgnoreCase
                    );
            });
        }
    }
}
