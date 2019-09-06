using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using ConsoleHttpWebServer.Models;
using Newtonsoft.Json;

namespace ConsoleHttpWebServer.Controllers
{
    class VoteController : BaseController
    {
        public VoteController(ParticipantRepository repository)
            : base(repository) { }

        public override void Handle(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

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
                var attend = keys["attend"] == "on";

                if (!string.IsNullOrEmpty(name))
                {
                    Repository.Add(new Participant(name, attend));

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
