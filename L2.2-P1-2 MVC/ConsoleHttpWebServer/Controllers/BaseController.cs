using System.IO;
using System.Net;
using ConsoleHttpWebServer.Models;

namespace ConsoleHttpWebServer.Controllers
{
    abstract class BaseController
    {
        public ParticipantRepository Repository { get; set; }

        protected BaseController(ParticipantRepository repository)
        {
            Repository = repository;
        }

        //public abstract StreamReader GetView(string viewName);
        public abstract void Handle(HttpListenerContext context);
    }
}
