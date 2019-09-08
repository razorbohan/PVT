using System;
using System.Collections.Generic;

namespace ConsoleHttpWebServer.Infrastructure
{
    static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();

        public static void Register<T>(Type serviceType)
        {
            Services[typeof(T)] = Activator.CreateInstance(serviceType);
        }

        public static T Resolve<T>()
        {
            return (T)Services[typeof(T)];
        }
    }
}
