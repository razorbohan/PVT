using System.Collections.Generic;
using System.Linq;
using ConsoleHttpWebServer.DAL;
using ConsoleHttpWebServer.Models;

namespace ConsoleHttpWebServer.Logic
{
    interface IParticipantsService
    {
        void Vote(string name, bool isAttend, string reason);
        List<Participant> ListAll();
        List<Participant> ListAttendent();
        List<Participant> ListMissed();
    }

    class ParticipantsService : IParticipantsService
    {
        private IParticipantsRepository Repository { get; }

        public ParticipantsService(IParticipantsRepository repository)
        {
            Repository = repository;
        }

        public void Vote(string name, bool isAttend, string reason)
        {
            Repository.Save(name, isAttend, reason);
        }

        public List<Participant> ListAll()
        {
            return Repository.List();
        }

        public List<Participant> ListAttendent()
        {
            return Repository.List().Where(x => x.IsAttend).ToList();
        }

        public List<Participant> ListMissed()
        {
            return Repository.List().Where(x => !x.IsAttend).ToList();
        }
    }
}
