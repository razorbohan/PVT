using System.Collections.Generic;
using System.IO;
using System.Linq;
using L6_P2_4_TagHelper.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace L6_P2_4_TagHelper.DAL
{
    public interface IParticipantsRepository
    {
        List<Participant> List();
        void Save(int partyId, string name, bool isAttend, string photo);
        void Delete(string name);
    }
    
    public class ParticipantsRepository : IParticipantsRepository
    {
        private List<Participant> Participants { get; }

        public ParticipantsRepository()
        {
            var json = File.ReadAllText("Data/participants.json");
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

        public void Save(int partyId, string name, bool isAttend, string photo)
        {
            var participant = Get(name);
            if (participant != null)
            {
                Delete(name);
            }

            var newParticipant = new Participant(partyId, name, isAttend, photo);
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
            File.WriteAllText("Data/participants.json", json);
        }
    }
}
