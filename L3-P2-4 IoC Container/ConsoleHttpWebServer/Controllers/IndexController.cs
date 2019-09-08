using System.IO;
using System.Net;
using ConsoleHttpWebServer.Infrastructure;
using ConsoleHttpWebServer.Logic;

namespace ConsoleHttpWebServer.Controllers
{
    class IndexController : BaseController
    {
        public IndexController(IParticipantsService service, ILogger logger)
            : base(service, logger) { }

        public override void Handle(HttpListenerContext context)
        {
            var response = context.Response;
            var request = context.Request;

            Logger.Info(request.Url.ToString());

            response.ContentType = "text/html";

            var fullFilePath = Path.GetFullPath(Path.Combine("Views", "Index.html"));

            using (var fileStream = File.OpenRead(fullFilePath))
            {
                response.ContentLength64 = new FileInfo(fullFilePath).Length;
                fileStream.CopyTo(response.OutputStream);
            }
        }
    }
}
