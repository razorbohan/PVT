using System;
using System.IO;

namespace L6_P2_4_TagHelper.Infrastructure
{
    public interface ILogger
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message);
    }

    public class Logger : ILogger
    {
        private string LogPath { get; }

        public Logger()
        {
            LogPath = "~/log.txt";
        }

        public Logger(string logPath)
        {
            LogPath = logPath;
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
