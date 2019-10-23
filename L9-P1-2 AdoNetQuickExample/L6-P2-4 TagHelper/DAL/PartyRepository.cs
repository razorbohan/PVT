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
        Party GetById(int id);
        List<Party> GetAllParties();
    }

    public class PartyRepository : IPartyRepository
    {
        private SqlConnection DbConnection { get; }
        //private List<Party> Parties { get; set; }
        private ILogger Logger { get; }

        //public PartyRepository()
        //{
        //    var json = File.ReadAllText("Data/parties.json");
        //    Parties = JsonConvert.DeserializeObject<List<Party>>(json);
        //}

        public PartyRepository(IConfiguration configuration, ILogger logger)
        {
            DbConnection = new SqlConnection(configuration.GetConnectionString("PartyDB"));
            Logger = logger;

            OpenConnection();
        }


        public Party GetById(int id)
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

        public List<Party> GetAllParties()
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

        private void OpenConnection()
        {
            if (DbConnection.State != ConnectionState.Open)
                DbConnection.Open();
        }
        
        private SqlDataReader ExecuteReader(string sqlQuery)
        {
            try
            {
                using (var transaction = DbConnection.BeginTransaction())
                using (var command = new SqlCommand { Transaction = transaction, CommandText = sqlQuery, Connection = DbConnection })
                {
                    var reader = command.ExecuteReader();
                    return reader;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
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
                Title = partyName?.ToString(),
                Location = partyLocation?.ToString(),
                Date = DateTime.Parse(partyDate?.ToString())
            };
        }
    }
}