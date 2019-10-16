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
            var json = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/parties.json"));
            Parties = JsonConvert.DeserializeObject<List<Party>>(json);
            //Parties = new List<Party>
            //{
            //    new Party {Id = 1, Title = "Super Party", Location = "Wall str 17", Date = DateTime.Now},
            //    new Party {Id = 2, Title = "Uber Party", Location = "Wall str 18", Date = DateTime.Now.AddDays(1)},
            //    new Party {Id = 3, Title = "After Party", Location = "Wall str 19", Date = DateTime.Now.AddDays(2)}
            //};
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