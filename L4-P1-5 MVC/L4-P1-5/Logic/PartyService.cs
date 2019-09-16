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
        Party GetParty(int partyId);
        //void Vote(int partyId, string name, bool isAttend, string reason);
        List<Participant> ListAttendants(int partyId);
        //List<Participant> ListAll();
        //List<Participant> ListMissed();
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

        public List<Party> GetIncomingParties()
        {
            return PartyRepository.List();
        }

        public Party GetParty(int partyId)
        {
            return PartyRepository.Get(partyId);
        }

        public List<Participant> ListAttendants(int partyId)
        {
            return ParticipantsRepository.List().Where(x => x.PartyId == partyId && x.IsAttend).ToList();
        }

        //public void Vote(int partyId, string name, bool isAttend, string reason)
        public void Vote(Participant participant)
        {
            //ParticipantsRepository.Save(partyId, name, isAttend, reason);
        }

        //public List<Participant> ListMissed()
        //{
        //    return Repository.List().Where(x => !x.IsAttend).ToList();
        //}

        //public List<Participant> ListAll()
        //{
        //    return Repository.List();
        //}
    }
}