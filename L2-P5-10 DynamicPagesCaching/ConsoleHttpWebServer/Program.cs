using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleHttpWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var server = new HttpWebServer(8881))
            {
                server.Create();

                while (true)
                {
                    server.Listen();
                }
            }

        }
    }

    class HttpWebServer : IDisposable
    {
        public int Port { get; set; }
        private HttpListener HttpListener { get; set; }

        public HttpWebServer(int port)
        {
            Port = port;
        }

        public void Create()
        {
            HttpListener = new HttpListener();
            HttpListener.Prefixes.Add($"http://*:{Port}/");
            HttpListener.Start();

            Console.WriteLine($"Server is listening on {Port}...");
        }

        public void Listen()
        {
            var context = HttpListener.GetContext();
            var request = context.Request;
            var response = context.Response;

            try
            {
                var fileName = request.RawUrl.Substring(1);
                fileName = string.IsNullOrWhiteSpace(fileName) ? "index.html" : fileName;
                Console.WriteLine($"Client is looking for {fileName}");

                HandleResponse(response, fileName);
            }
            finally
            {
                response.OutputStream.Close();
            }
        }

        private void HandleResponse(HttpListenerResponse response, string fileName)
        {
            var fullFilePath = Path.GetFullPath(Path.Combine("wwwroot", fileName));
            if (!File.Exists(fullFilePath))
            {
                response.ContentLength64 = 0;
                response.StatusCode = 404;
                response.StatusDescription = "File not found";
                response.OutputStream.Close();

                return;
            }

            response.ContentType = "text/html";

            using (var fileStream = File.OpenRead(fullFilePath))
            {
                if (fileName == "participants.html")
                {
                    if (CheckIfCached("participants.html"))
                    {
                        Console.WriteLine($"Taken from cache: {fileName}");
                        using (var cachedStream = File.OpenRead("cache/participants.html"))
                        {
                            response.ContentLength64 = new FileInfo("cache/participants.html").Length;
                            cachedStream.CopyTo(response.OutputStream);
                        }
                    }
                    else
                    {
                        using (var pageStream = new StreamReader(fileStream))
                        {
                            var tag = "";

                            var json = File.ReadAllText("wwwroot/list.json");
                            var users = JsonConvert.DeserializeObject<List<Participant>>(json);
                            users.ForEach(user => tag += $"<li>{user.Name}</li>");

                            var page = pageStream.ReadToEnd().Replace("{{participants}}", tag);
                            File.WriteAllText("cache/participants.html", page);

                            var bytes = Encoding.UTF8.GetBytes(page);
                            response.ContentLength64 = bytes.Length;

                            new MemoryStream(bytes).CopyTo(response.OutputStream);
                        }
                    }
                }
                else
                {
                    response.ContentLength64 = new FileInfo(fullFilePath).Length;
                    fileStream.CopyTo(response.OutputStream);
                }
            }
        }

        private bool CheckIfCached(string filename)
        {
            if (!Directory.Exists("cache"))
                Directory.CreateDirectory("cache");

            var modification = File.GetLastWriteTime($@"cache\{filename}");
            return (DateTime.Now - modification).TotalMinutes < 2;
        }

        public void Dispose()
        {
            HttpListener.Stop();
        }
    }

    class Participant
    {
        public string Name { get; set; }
    }
}
