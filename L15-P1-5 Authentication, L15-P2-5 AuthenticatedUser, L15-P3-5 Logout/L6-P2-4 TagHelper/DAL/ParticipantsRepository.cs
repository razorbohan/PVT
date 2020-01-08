using System.Linq;
using L6_P2_4_TagHelper.Models;
using Microsoft.EntityFrameworkCore;

namespace L6_P2_4_TagHelper.DAL
{
    public interface IParticipantRepository
    {
        Participant Get(int id);
        IQueryable<Participant> GetAll();
        void Create(Participant participant);
        void Delete(int id);
        void Edit(Participant participant);
    }

    public class ParticipantRepository : IParticipantRepository
    {
        private PartyContext Context { get; }

        public ParticipantRepository(PartyContext context)
        {
            Context = context;
        }

        public Participant Get(int id)
        {
            return Context.Participants.FirstOrDefault(participant => participant.Id == id);
        }

        public IQueryable<Participant> GetAll()
        {
            return Context.Participants;
        }

        public void Create(Participant participant)
        {
            Context.Participants.Add(participant);
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var participant = Get(id);

            Context.Participants.Remove(participant);
            Context.SaveChanges();
        }

        public void Edit(Participant participant)
        {
            Context.Participants.Update(participant);
            Context.SaveChanges();
        }
    }
}
