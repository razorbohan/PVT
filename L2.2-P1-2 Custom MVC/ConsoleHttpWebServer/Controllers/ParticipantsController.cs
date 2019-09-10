using System.IO;
using System.Net;
using System.Text;
using ConsoleHttpWebServer.Models;

namespace ConsoleHttpWebServer.Controllers
{
    class ParticipantsController : BaseController
    {
        public ParticipantsController(ParticipantRepository repository)
            : base(repository) { }

        public override void Handle(HttpListenerContext context)
        {
            var response = context.Response;
            response.ContentType = "text/html";

            var fullFilePath = Path.GetFullPath(Path.Combine("Views", "Participants.html"));

            using (var fileStream = new StreamReader(fullFilePath))
            {
                var users = Repository.GetAll();

                var tag = "";
                users.ForEach(user => tag += $"<li>{user.Name}</li>");

                var page = fileStream.ReadToEnd().Replace("{{participants}}", tag);
                //File.WriteAllText("cache/participants.html", page);
                
                var bytes = Encoding.UTF8.GetBytes(page);
                response.ContentLength64 = bytes.Length;

                new MemoryStream(bytes).CopyTo(response.OutputStream);
            }
        }
    }
}
