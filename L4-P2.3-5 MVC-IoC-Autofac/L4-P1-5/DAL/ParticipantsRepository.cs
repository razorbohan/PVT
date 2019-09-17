using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using L4_P1_5.Models;
using Newtonsoft.Json;

namespace L4_P1_5.DAL
{
    public interface IParticipantsRepository
    {
        List<Participant> List();
        void Save(int partyId, string name, bool isAttend);
        void Delete(string name);
    }
    
    public class ParticipantsRepository : IParticipantsRepository
    {
        private List<Participant> Participants { get; }

        public ParticipantsRepository()
        {
            var json = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/participants.json"));
            Participants = JsonConvert.DeserializeObject<List<Participant>>(json);
        }

        public List<Participant> List()
        {
            return Participants;
        }

        public Participant Get(string name)
        {
            return Participants.FirstOrDefault(x => x.Name == name);
        }

        public void Save(int partyId, string name, bool isAttend)
        {
            var participant = Get(name);
            if (participant != null)
            {
                Delete(name);
            }

            var newParticipant = new Participant(partyId, name, isAttend);
            Participants.Add(newParticipant);

            Commit();
        }

        public void Delete(string name)
        {
            var participant = Get(name);
            Participants.Remove(participant);

            Commit();
        }

        private void Commit()
        {
            var json = JsonConvert.SerializeObject(Participants);
            File.WriteAllText(HttpContext.Current.Server.MapPath("~/App_Data/participants.json"), json);
        }
    }
}
