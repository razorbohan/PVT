using System.IO;
using System.Net;
using System.Web;
using ConsoleHttpWebServer.Infrastructure;
using ConsoleHttpWebServer.Logic;
using ConsoleHttpWebServer.Models;

namespace ConsoleHttpWebServer.Controllers
{
    class VoteController : BaseController
    {
        public VoteController(IParticipantsService service, ILogger logger)
            : base(service, logger) { }

        public override void Handle(HttpListenerContext context)
        {
            var response = context.Response;
            var request = context.Request;

            Logger.Info(request.Url.ToString());

            if (request.HttpMethod == "GET")
            {
                response.ContentType = "text/html";

                var fullFilePath = Path.GetFullPath(Path.Combine("Views", "Vote.html"));

                using (var fileStream = File.OpenRead(fullFilePath))
                {
                    response.ContentLength64 = new FileInfo(fullFilePath).Length;
                    fileStream.CopyTo(response.OutputStream);
                }
            }
            else if (request.HttpMethod == "POST")
            {
                var body = new StreamReader(request.InputStream).ReadToEnd();
                var keys = HttpUtility.ParseQueryString(body);

                var name = keys["name"];
                var isAttend = keys["attend"] == "on";
                var reason = keys["reason"];

                if (!string.IsNullOrEmpty(name))
                {
                    Service.Vote(name, isAttend, reason);

                    //GeneratePage();

                    response.Redirect("participants.html");
                }
                else
                {
                    response.Redirect("vote.html");
                }
            }
        }
    }
}
