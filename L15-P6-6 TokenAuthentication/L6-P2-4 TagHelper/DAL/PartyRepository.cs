using System.Collections.Generic;
using System.Linq;
using L6_P2_4_TagHelper.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace L6_P2_4_TagHelper.DAL
{
    public interface IPartyRepository
    {
        Party Get(int id);
        List<Party> GetAll();
        void Create(Party party);
        void Delete(int id);
        void Edit(Party party);
    }

    public class PartyRepository : IPartyRepository
    {
        private PartyContext Context { get; }

        public PartyRepository(PartyContext context)
        {
            Context = context;
        }

        public Party Get(int id)
        {
            return Context.Parties.FirstOrDefault(party => party.Id == id);
        }

        public Party GetByName(string name)
        {
            return Context.Parties.FirstOrDefault(party => party.Name == name);
        }

        public List<Party> GetAll()
        {
            return Context.Parties.ToList();
        }

        public void Create(Party party)
        {
            Context.Parties.Add(party);
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var party = Get(id);

            Context.Parties.Remove(party);
            Context.SaveChanges();
        }

        public void Edit(Party party)
        {
            Context.Parties.Update(party);
            Context.SaveChanges();
        }
    }
}
