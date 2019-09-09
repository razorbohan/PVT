using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleHttpWebServer.Infrastructure
{
    static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();

        public static void Register<T>(Type serviceType)
        {
            var ctorParams = serviceType.GetConstructors().First().GetParameters();
            if (ctorParams.Length == 0)
                Services[typeof(T)] = Activator.CreateInstance(serviceType);
            else
            {
                var dependencies = CreateDependencies(ctorParams);
                Services[typeof(T)] = Activator.CreateInstance(serviceType, dependencies);
            }
        }

        private static object[] CreateDependencies(ParameterInfo[] parameters)
        {
            //return parameters.Select(parameter => Activator.CreateInstance(parameter.ParameterType)).ToArray();
            var dependencies = new List<object>();
            foreach (var parameter in parameters)
            {
                var dependency = Activator.CreateInstance(parameter.ParameterType);
                ServiceLocator.Resolve<parameter.ParameterType>();
                dependencies.Add(dependency);
            }

            return dependencies.ToArray();
        }

        public static T Resolve<T>()
        {
            return (T)Services[typeof(T)];
        }
    }
}
