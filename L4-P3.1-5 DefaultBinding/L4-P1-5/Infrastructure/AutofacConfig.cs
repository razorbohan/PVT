using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using L4_P1_5.DAL;
using L4_P1_5.Logic;

namespace L4_P1_5.Infrastructure
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<Logger>().As<ILogger>();
            builder.RegisterType<ParticipantsRepository>().As<IParticipantsRepository>();
            builder.RegisterType<PartyRepository>().As<IPartyRepository>();
            builder.RegisterType<PartyService>().As<IPartyService>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}