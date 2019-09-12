using System.IO;
using System.Net;
using System.Text;

namespace ConsoleHttpWebServer.Controllers
{
    /*class ParticipantsController : BaseController
    {
        public ParticipantsController(IPartyService service, ILogger logger)
            : base(service, logger) { }

        public override void Handle(HttpListenerContext context)
        {
            var response = context.Response;
            var request = context.Request;

            Logger.Info(request.Url.ToString());

            response.ContentType = "text/html";

            var fullFilePath = Path.GetFullPath(Path.Combine("Views", "Participants.html"));

            using (var fileStream = new StreamReader(fullFilePath))
            {
                var users = Service.ListAttendent();

                var tag = "";
                users.ForEach(user => tag += $"<li>{user.Title}</li>");

                var page = fileStream.ReadToEnd().Replace("{{participants}}", tag);
                //File.WriteAllText("cache/participants.html", page);
                
                var bytes = Encoding.UTF8.GetBytes(page);
                response.ContentLength64 = bytes.Length;

                new MemoryStream(bytes).CopyTo(response.OutputStream);
            }
        }
    }*/
}
