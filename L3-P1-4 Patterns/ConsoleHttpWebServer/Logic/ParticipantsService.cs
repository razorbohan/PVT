using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleHttpWebServer.Models;

namespace ConsoleHttpWebServer.Logic
{
    interface IParticipantsService
    {
        void Vote();
        List<Participant> ListAll();
        List<Participant> ListAttendent();
        List<Participant> ListMissed();
    }

    class ParticipantsService : IParticipantsService
    {
        private IParticipantRepository Repository { get; set; }

        public ParticipantsService(IParticipantRepository repository)
        {
            Repository = repository;
        }

        public void Vote(string name, bool isAttend, string reason)
        {
            var participant = new Participant(name, isAttend, reason);
            Repository.Add(participant);
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
            return Repository.List().Where(x => x.IsAttend).ToList();
        }
    }
}
