using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace L2._2_P2_2_RazorPages.Models
{
    public interface IParticipantRepository
    {
        List<Participant> GetAll();
        void Add(Participant participant);
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

        public List<Participant> GetAll()
        {
            return Participants;
        }

        public void Add(Participant participant)
        {
            Participants.Add(participant);

            var json = JsonConvert.SerializeObject(Participants);
            File.WriteAllText("data.json", json);
        }
    }
}
