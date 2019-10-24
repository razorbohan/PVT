using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using L6_P2_4_TagHelper.Infrastructure;
using L6_P2_4_TagHelper.Models;

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

    public class PartyRepository : Repository, IPartyRepository
    {
        public PartyRepository(IConfiguration configuration, ILogger logger)
            : base(configuration, logger)
        {
            //Create(new Party { Title = "God Party", Location = "San Morino", Date = DateTime.Now.AddDays(1) });
            //Edit(new Party { Id = 19, Title = "Hot Party1", Location = "Wall str 351", Date = DateTime.Now.AddDays(1) });
            //Delete(22);
        }

        public Party Get(int id)
        {
            var sqlQuery = $"SELECT * FROM Parties WHERE Id='{id}'";
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

        public Party GetByName(string name)
        {
            var sqlQuery = $"SELECT * FROM Parties WHERE Name='{name}'";
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

        public List<Party> GetAll()
        {
            var parties = new List<Party>();

            var sqlQuery = "SELECT * FROM Parties";
            var reader = ExecuteReader(sqlQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    parties.Add(ReaderToObject(reader));
                }
            }

            reader.Close();
            return parties;
        }

        public List<Party> GetFututeParties()
        {
            var futureParties = new List<Party>();

            var sqlQuery = "SELECT * FROM Parties where Date > GETDATE()";
            var reader = ExecuteReader(sqlQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    futureParties.Add(ReaderToObject(reader));
                }
            }

            reader.Close();
            return futureParties;
        }

        public List<Party> GetNearestParties(int limit)
        {
            var futureParties = new List<Party>();

            var sqlQuery = $"SELECT TOP {limit} * from dbo.Parties WHERE Date > GETDATE() ORDER BY Date Desc";
            var reader = ExecuteReader(sqlQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    futureParties.Add(ReaderToObject(reader));
                }
            }

            reader.Close();
            return futureParties;
        }

        public void Create(Party party)
        {
            ExecuteNonQuery($"INSERT INTO Parties (Name, Location, Date) VALUES ('{party.Name}', '{party.Location}', '{party.Date:yyyy-MM-dd HH:mm:ss.fff}');");
        }

        public void Delete(int id)
        {
            ExecuteNonQuery($"DELETE FROM Parties WHERE id = {id}");
        }

        public void Edit(Party party)
        {
            ExecuteNonQuery($"UPDATE Parties SET Name='{party.Name}', Location='{party.Location}', Date='{party.Date:yyyy-MM-dd HH:mm:ss.fff}' WHERE id = {party.Id};");
        }

        private Party ReaderToObject(SqlDataReader reader)
        {
            var partyId = reader.GetValue(0);
            var partyName = reader.GetValue(1);
            var partyLocation = reader.GetValue(2);
            var partyDate = reader.GetValue(3);

            return new Party
            {
                Id = (int)partyId,
                Name = partyName?.ToString(),
                Location = partyLocation?.ToString(),
                Date = DateTime.Parse(partyDate?.ToString())
            };
        }
    }
}
