using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace L4_P1_5.Infrastructure
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public void Register(Type interfaceType, Type serviceType)
        {
            var ctorParams = serviceType.GetConstructors().First().GetParameters();
            if (ctorParams.Length == 0)
                _services[interfaceType] = Activator.CreateInstance(serviceType);
            else
            {
                var dependencies = CreateDependencies(ctorParams);
                _services[interfaceType] = Activator.CreateInstance(serviceType, dependencies);
            }
        }

        public object Resolve(Type serviceType)
        {
            _services.TryGetValue(serviceType, out var service);
            return service;
        }

        private object[] CreateDependencies(ParameterInfo[] parameters)
        {
            return parameters.Select(parameter => Resolve(parameter.ParameterType)).ToArray();
        }
    }
}
