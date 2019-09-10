using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleHttpWebServer.Controllers;
using ConsoleHttpWebServer.DAL;
using ConsoleHttpWebServer.Infrastructure;
using ConsoleHttpWebServer.Logic;
using ConsoleHttpWebServer.Models;

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
        private List<WebSocket> WebSocketClients { get; set; }

        public HttpWebServer(int port)
        {
            Port = port;
            WebSocketClients = new List<WebSocket>();
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

            RegisterServices();

            var logger = ServiceLocator.Resolve(typeof(ILogger)) as ILogger;
            //var repository = ServiceLocator.Resolve(typeof(IParticipantsRepository)) as IParticipantsRepository;
            var service = ServiceLocator.Resolve(typeof(IParticipantsService)) as IParticipantsService;

            var indexController = new IndexController(service, logger);
            var voteController = new VoteController(service, logger);
            var participantsController = new ParticipantsController(service, logger);

            if (context.Request.IsWebSocketRequest)
            {
                HandleWebsocket(context);
            }
            else
            {
                try
                {
                    var fileName = request.RawUrl.Substring(1);
                    fileName = string.IsNullOrWhiteSpace(fileName) ? "index.html" : fileName;
                    Console.WriteLine($"Client is looking for {fileName}");

                    switch (fileName)
                    {
                        case "index.html":
                            indexController.Handle(context);
                            break;
                        case "vote.html":
                            voteController.Handle(context);
                            break;
                        case "participants.html":
                            participantsController.Handle(context);
                            break;
                        default:
                            HandleStaticFile(context);
                            break;
                    }
                }
                finally
                {
                    response.OutputStream.Close();
                }
            }
        }

        private void HandleStaticFile(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;
            var fileName = request.RawUrl.Substring(1);

            if (fileName.EndsWith(".css"))
                response.ContentType = "text/css";
            else if (fileName.EndsWith(".js"))
                response.ContentType = "text/javascript";


            var fullFilePath = Path.GetFullPath(Path.Combine("wwwroot", fileName));

            using (var fileStream = File.OpenRead(fullFilePath))
            {
                response.ContentLength64 = new FileInfo(fullFilePath).Length;
                fileStream.CopyTo(response.OutputStream);
            }
        }

        private async Task HandleWebsocket(HttpListenerContext listenerContext)
        {
            WebSocketContext webSocketContext;
            try
            {
                webSocketContext = await listenerContext.AcceptWebSocketAsync(null);
                //Interlocked.Increment(ref count);
                //Console.WriteLine("Processed: {0}", count);
            }
            catch (Exception e)
            {
                listenerContext.Response.StatusCode = 500;
                listenerContext.Response.Close();
                Console.WriteLine("Exception: {0}", e);
                return;
            }

            var webSocket = webSocketContext.WebSocket;
            WebSocketClients.Add(webSocket);

            try
            {
                byte[] receiveBuffer = new byte[1024];

                while (webSocket.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);

                    if (receiveResult.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    }
                    else if (receiveResult.MessageType == WebSocketMessageType.Text)
                    {
                        var receivedString = Encoding.UTF8.GetString(receiveBuffer, 0, receiveResult.Count);
                        WebSocketClients.ForEach(async client => await client.SendAsync(
                            new ArraySegment<byte>(Encoding.UTF8.GetBytes(receivedString)),
                            WebSocketMessageType.Text,
                            true,
                            CancellationToken.None)
                        );
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            finally
            {
                webSocket?.Dispose();
            }
        }

        //private bool CheckIfCached(string filename)
        //{
        //    if (!Directory.Exists("cache"))
        //        Directory.CreateDirectory("cache");

        //    var modification = File.GetLastWriteTime($@"cache\{filename}");
        //    return (DateTime.Now - modification).TotalMinutes < 2;
        //}

        void RegisterServices()
        {
            ServiceLocator.Register(typeof(ILogger), typeof(Logger));
            ServiceLocator.Register(typeof(IParticipantsRepository), typeof(ParticipantsRepository));
            ServiceLocator.Register(typeof(IParticipantsService), typeof(ParticipantsService));
        }

        public void Dispose()
        {
            HttpListener.Stop();
        }
    }
}
