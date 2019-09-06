using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleHttpWebServer.Logic
{
    interface ILogger
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message);
    }

    class Logger : ILogger
    {
        private string LogPath { get; }

        public Logger(string logPath)
        {
            LogPath = logPath ?? "log.txt";
        }

        public void Error(string message)
        {
            File.AppendAllText(LogPath, $"{DateTime.Now} ERROR {message}{Environment.NewLine}");
        }

        public void Info(string message)
        {
            File.AppendAllText(LogPath, $"{DateTime.Now} INFO {message}{Environment.NewLine}");
        }

        public void Warn(string message)
        {
            File.AppendAllText(LogPath, $"{DateTime.Now} WARN {message}{Environment.NewLine}");
        }
    }
}
