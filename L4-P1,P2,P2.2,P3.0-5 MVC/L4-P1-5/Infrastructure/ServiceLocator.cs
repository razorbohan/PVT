using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace L4_P1_5.Infrastructure
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, (object instance, bool isTransient)> _services = new Dictionary<Type, (object, bool)>();

        public void Register(Type interfaceType, Type serviceType, bool isTransient)
        {
            var instance = CreateInstance(serviceType);
            _services[interfaceType] = (instance, isTransient);
        }

        public object Resolve(Type serviceType)
        {
            _services.TryGetValue(serviceType, out var service);

            if (service.isTransient)
            {
                return CreateInstance(service.instance.GetType());
            }

            return service.instance;
        }

        private object CreateInstance(Type serviceType)
        {
            var ctorParams = serviceType.GetConstructors().First().GetParameters();
            if (ctorParams.Length != 0)
            {
                var dependencies = CreateDependencies(ctorParams);
                return Activator.CreateInstance(serviceType, dependencies);
            }

            return Activator.CreateInstance(serviceType);
        }

        private object[] CreateDependencies(ParameterInfo[] parameters)
        {
            return parameters.Select(parameter => Resolve(parameter.ParameterType)).ToArray();
        }
    }
}
