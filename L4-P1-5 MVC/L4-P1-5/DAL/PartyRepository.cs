using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using L4_P1_5.Models;

namespace L4_P1_5.DAL
{
    public interface IPartyRepository
    {
        Party Get(int id);
        List<Party> List();
    }

    public class PartyRepository : IPartyRepository
    {
        private List<Party> Parties { get; set; }

        public PartyRepository()
        {
            Parties = new List<Party>
            {
                new Party {Id = 0, Title = "Uber Party", Location = "Wall str 17", Date = DateTime.Now},
                new Party {Id = 1, Title = "After Party", Location = "Wall str 19", Date = DateTime.Now.AddDays(1)}
            };
        }

        public List<Party> List()
        {
            return Parties;
        }

        public Party Get(int id)
        {
            return Parties.FirstOrDefault(x => x.Id == id);
        }
    }
}