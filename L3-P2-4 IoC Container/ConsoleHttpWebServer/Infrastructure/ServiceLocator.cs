using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleHttpWebServer.Infrastructure
{
    static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();

        public static void Register(Type interfaceType, Type serviceType)
        {
            var ctorParams = serviceType.GetConstructors().First().GetParameters();
            if (ctorParams.Length == 0)
                Services[interfaceType] = Activator.CreateInstance(serviceType);
            else
            {
                var dependencies = CreateDependencies(ctorParams);
                Services[interfaceType] = Activator.CreateInstance(serviceType, dependencies);
            }
        }

        public static object Resolve(Type serviceType)
        {
            return Services[serviceType];
        }

        private static object[] CreateDependencies(ParameterInfo[] parameters)
        {
            return parameters.Select(parameter => Resolve(parameter.ParameterType)).ToArray();
        }
    }
}
