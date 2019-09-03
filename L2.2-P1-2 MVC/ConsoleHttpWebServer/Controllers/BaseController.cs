using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ConsoleHttpWebServer.Controllers
{
    abstract class BaseController
    {
        public abstract string Handle(HttpListenerRequest request, HttpListenerResponse response);

        protected string GetView(string viewName)
        {
            return "";
        }
    }
}
