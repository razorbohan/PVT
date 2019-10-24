using System.Collections.Generic;
using System.Linq;
using L6_P2_4_TagHelper.DAL;
using L6_P2_4_TagHelper.Models;

namespace L6_P2_4_TagHelper.Logic
{
    public interface IPartyService
    {
        List<Party> GetIncomingParties();
        List<Party> GetFirst10Parties();
        Party GetParty(int partyId);
        void Vote(int partyId, string name, bool isAttend, string photo);
        List<Participant> ListAttendants(int partyId);
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
            return PartyRepository.GetAll();
        }

        public List<Party> GetFirst10Parties()
        {
            return PartyRepository.GetAll().OrderByDescending(x => x.Date).Take(10).ToList();
        }

        public Party GetParty(int partyId)
        {
            return PartyRepository.Get(partyId);
        }

        public List<Participant> ListAttendants(int partyId)
        {
            return ParticipantsRepository.GetAll().Where(x => x.PartyId == partyId && x.IsAttend).ToList();
        }

        public void Vote(int partyId, string name, bool isAttend, string photo)
        {
            ParticipantsRepository.Create(new Participant { Name = name, IsAttend = isAttend, Avatar = photo, PartyId = partyId });
        }
    }
}