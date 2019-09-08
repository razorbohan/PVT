using System.Diagnostics;

namespace ConsoleHttpWebServer.Models
{
    class Participant
    {
        public string Name { get; set; }
        public bool IsAttend { get; set; }
        public string Reason { get; set; }

        public Participant(string name, bool isAttend, string reason)
        {
            Name = name;
            IsAttend = isAttend;
            Reason = reason;
        }
    }
}
