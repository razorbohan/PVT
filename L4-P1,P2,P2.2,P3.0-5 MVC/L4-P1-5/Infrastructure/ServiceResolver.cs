using System;
using System.Collections.Generic;
using System.Web.Mvc;
using L4_P1_5.Controllers;
using L4_P1_5.DAL;
using L4_P1_5.Logic;

namespace L4_P1_5.Infrastructure
{
    public class ServiceResolver : IDependencyResolver
    {
        private IDependencyResolver DependencyResolver { get; set; }
        private ServiceLocator ServiceLocator { get; set; }

        public ServiceResolver(IDependencyResolver dependencyResolver)
        {
            DependencyResolver = dependencyResolver;

            ServiceLocator = new ServiceLocator();
            RegisterServices();
        }

        public object GetService(Type serviceType)
        {
            return ServiceLocator.Resolve(serviceType) ?? DependencyResolver.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return DependencyResolver.GetServices(serviceType);
        }

        private void RegisterServices()
        {
            ServiceLocator.Register(typeof(ILogger), typeof(Logger), isTransient: false);
            ServiceLocator.Register(typeof(IParticipantsRepository), typeof(ParticipantsRepository), isTransient: false);
            ServiceLocator.Register(typeof(IPartyRepository), typeof(PartyRepository), isTransient: false);
            ServiceLocator.Register(typeof(IPartyService), typeof(PartyService), isTransient: false);

            ServiceLocator.Register(typeof(HomeController), typeof(HomeController), isTransient: true);
            ServiceLocator.Register(typeof(PartyController), typeof(PartyController), isTransient: true);
        }
    }
}