using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using L6_P2_4_TagHelper.Infrastructure;
using Newtonsoft.Json;
using L6_P2_4_TagHelper.Models;
using Microsoft.Extensions.Configuration;

namespace L6_P2_4_TagHelper.DAL
{
    public interface IParticipantsRepository
    {
        //List<Participant> GetAll();
        //void Save(int partyId, string name, bool isAttend, string photo);
        //void Delete(string name);

        Participant Get(string name);
        List<Participant> GetAll();
        void Create(Participant participant);
        void Delete(int id);
        void Edit(Participant participant);
    }

    public class ParticipantsRepository : Repository, IParticipantsRepository
    {
        public ParticipantsRepository(IConfiguration configuration, ILogger logger)
            : base(configuration, logger)
        {
            //var all = GetAll();
            //var one = Get("Valery");
            //Create(new Participant { Name = "Eugen", IsAttend = true, Avatar = null, Reason = null, PartyId = 2});
            //Edit(new Participant { Id = 10, Name = "Eugen1", IsAttend = false, Avatar = "c6e0609a-fe17-4ac8-9c2b-ff80d1a8a4ae.jpg", Reason = "because i am ill", PartyId = 3});
            //Delete(10);
        }

        public Participant Get(string name)
        {
            var sqlQuery = $"SELECT * FROM Participants WHERE Name='{name}'";
            var reader = ExecuteReader(sqlQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    return ReaderToObject(reader);
                }
            }

            reader.Close();
            return null;
        }

        public List<Participant> GetAll()
        {
            var participants = new List<Participant>();

            var sqlQuery = "SELECT * FROM Participants";
            var reader = ExecuteReader(sqlQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    participants.Add(ReaderToObject(reader));
                }
            }

            reader.Close();
            return participants;
        }

        public void Create(Participant participant)
        {
            ExecuteNonQuery($"INSERT INTO Participants (Name, IsAttend, Avatar, Reason, PartyId) VALUES ('{participant.Name}', '{participant.IsAttend}', '{participant.Avatar}','{participant.Reason}','{participant.PartyId}')");
        }

        public void Delete(int id)
        {
            ExecuteNonQuery($"DELETE FROM Participants WHERE id = {id}");
        }

        public void Edit(Participant participant)
        {
            ExecuteNonQuery($"UPDATE Participants SET Name='{participant.Name}', IsAttend='{participant.IsAttend}', Avatar='{participant.Avatar}', Reason='{participant.Reason}', PartyId='{participant.PartyId}' WHERE id = {participant.Id}");
        }

        private Participant ReaderToObject(SqlDataReader reader)
        {
            var id = reader.GetValue(0);
            var name = reader.GetValue(1);
            var isAttend = reader.GetValue(2);
            var avatar = reader.GetValue(3);
            var reason = reader.GetValue(4);
            var partyId = reader.GetValue(5);

            return new Participant
            {
                Id = (int)id,
                Name = name?.ToString(),
                IsAttend = (bool)isAttend,
                Avatar = avatar?.ToString(),
                Reason = reason?.ToString(),
                PartyId = (int)partyId
            };
        }
    }
}
