using System.IO;
using System.Net;
using ConsoleHttpWebServer.Models;

namespace ConsoleHttpWebServer.Controllers
{
    class IndexController : BaseController
    {
        public IndexController(ParticipantRepository repository)
            : base(repository) { }

        public override void Handle(HttpListenerContext context)
        {
            var response = context.Response;
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
