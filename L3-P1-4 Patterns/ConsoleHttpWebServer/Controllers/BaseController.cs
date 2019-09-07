using System.Net;
using ConsoleHttpWebServer.Infrastructure;
using ConsoleHttpWebServer.Logic;

namespace ConsoleHttpWebServer.Controllers
{
    abstract class BaseController
    {
        public IParticipantsService Service { get; set; }
        public ILogger Logger { get; set; }    

        protected BaseController(IParticipantsService service, ILogger logger)
        {
            Service = service;
            Logger = logger;
        }

        //public abstract StreamReader GetView(string viewName);
        public abstract void Handle(HttpListenerContext context);
    }
}
