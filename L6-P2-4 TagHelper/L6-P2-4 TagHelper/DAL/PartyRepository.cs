using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using L6_P2_4_TagHelper.Models;
using Newtonsoft.Json;

namespace L6_P2_4_TagHelper.DAL
{
    public interface IPartyRepository
    {
        Party Get(int id);
        List<Party> List();
    }

    public class PartyRepository : IPartyRepository
    {
        private List<Party> Parties { get; }

        public PartyRepository()
        {
            var json = File.ReadAllText("Data/parties.json");
            Parties = JsonConvert.DeserializeObject<List<Party>>(json);
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