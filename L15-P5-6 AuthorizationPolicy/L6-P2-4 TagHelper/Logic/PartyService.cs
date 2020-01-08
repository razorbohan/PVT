using System.Collections.Generic;
using System.Linq;
using L6_P2_4_TagHelper.DAL;
using L6_P2_4_TagHelper.Models;

namespace L6_P2_4_TagHelper.Logic
{
    public interface IPartyService
    {
        List<Party> GetIncomingParties();
        List<Party> GetAdultParties();
        List<Party> GetFirst10Parties();
        Party GetParty(int partyId);
        void Vote(int partyId, string name, bool isAttend, string photo);
        List<Participant> ListAttendants(int partyId);
    }

    public class PartyService : IPartyService
    {
        public IPartyRepository PartyRepository { get; set; }
        public IParticipantRepository ParticipantRepository { get; set; }

        public PartyService(IPartyRepository partyRepository, IParticipantRepository participantRepository)
        {
            PartyRepository = partyRepository;
            ParticipantRepository = participantRepository;
        }

        public List<Party> GetIncomingParties()
        {
            return PartyRepository.GetAll();
        }

        public List<Party> GetAdultParties()
        {
            return PartyRepository.GetAll().Where(x => x.IsPlus18).ToList();
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
            return ParticipantRepository.GetAll().Where(x => x.PartyId == partyId && x.IsAttend).ToList();
        }

        public void Vote(int partyId, string name, bool isAttend, string photo)
        {
            ParticipantRepository.Create(new Participant { Name = name, IsAttend = isAttend, Avatar = photo, PartyId = partyId });
        }
    }
}