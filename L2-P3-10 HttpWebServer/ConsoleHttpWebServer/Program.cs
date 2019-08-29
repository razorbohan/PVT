using System;
using System.IO;
using System.Net;

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

                var fullFilePath = Path.GetFullPath(Path.Combine("Files", fileName));
                if (!File.Exists(fullFilePath))
                {
                    response.ContentLength64 = 0;
                    response.StatusCode = 404;
                    response.StatusDescription = "File not found";
                    response.OutputStream.Close();

                    return;
                }

                using (var fileStream = File.OpenRead(fullFilePath))
                {
                    if (fileName == "index.html")
                    {
                        response.ContentType = "text/html";
                    }
                    else
                    {
                        response.ContentType = "application/octet-stream";
                        response.AddHeader("Content-Disposition", $"Attachment; filename=\"{fileName}\"");
                    }

                    response.ContentLength64 = new FileInfo(fullFilePath).Length;
                    fileStream.CopyTo(response.OutputStream);
                }
            }
            finally
            {
                response.OutputStream.Close();
            }
        }

        public void Dispose()
        {
            HttpListener.Stop();
        }
    }
}
