using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using L4_P1_5.DAL;
using L4_P1_5.Models;

namespace L4_P1_5.Logic
{
    public interface IPartyService
    {
        List<Party> GetIncomingParties();
        List<Participant> ListAll();
        List<Participant> ListAttendent();
        List<Participant> ListMissed();
        void Vote(string name, bool isAttend, string reason);
    }

    public class PartyService : IPartyService
    {
        public IPartyRepository PartyRepository { get; set; }
        public IParticipantsRepository ParticipantsRepository { get; set; }


        public PartyService(IPartyRepository partyRepository, IParticipantsRepository participantsRepository)
        {
            PartyRepository = partyRepository;
            ParticipantsRepository = participantsRepository;
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

        public List<Party> GetIncomingParties()
        {
            return null;
        }
    }
}