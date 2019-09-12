﻿using System.Collections.Generic;
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
        void Save(string name, bool isAttend, string reason);
        void Delete(string name);
    }
    
    public class ParticipantsRepository : IParticipantsRepository
    {
        private List<Participant> Participants { get; }

        public ParticipantsRepository()
        {
            var json = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/data.json"));
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

        public void Save(string name, bool isAttend, string reason)
        {
            var participant = Get(name);
            if (participant != null)
            {
                Delete(name);
            }

            var newParticipant = new Participant(name, isAttend, reason);
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
            File.WriteAllText("data.json", json);
        }
    }
}
