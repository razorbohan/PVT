using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleHttpWebServer.Models
{
    interface IParticipantRepository
    {
        List<Participant> List();
        void Add(Participant participant);
        void Delete(Participant participant);
    }
    
    class ParticipantRepository : IParticipantRepository
    {
        private List<Participant> Participants { get; set; }
        //private ParticipantRepository Repository { get; set; }

        //public ParticipantRepository GetRepository()
        //{
        //    return Repository ?? (Repository = new ParticipantRepository());
        //}

        public ParticipantRepository()
        {
            var json = File.ReadAllText("data.json");
            Participants = JsonConvert.DeserializeObject<List<Participant>>(json);
        }

        public List<Participant> List()
        {
            return Participants;
        }

        public void Add(Participant participant)
        {
            Participants.Add(participant);
            SaveToJson();
        }

        public void Delete(Participant participant)
        {
            Participants.Remove(participant);
            SaveToJson();
        }

        private void SaveToJson()
        {
            var json = JsonConvert.SerializeObject(Participants);
            File.WriteAllText("data.json", json);
        }
    }
}
