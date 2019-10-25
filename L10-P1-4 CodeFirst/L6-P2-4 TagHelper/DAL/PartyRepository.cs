using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;
using L6_P2_4_TagHelper.Infrastructure;
using L6_P2_4_TagHelper.Models;
using Microsoft.EntityFrameworkCore;

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

    public class PartyRepository : IPartyRepository
    {
        private PartyContext Context { get; }
        private DbSet<Party> Entities { get; }

        public PartyRepository(PartyContext context)
        {
            Context = context;
            Entities = context.Set<Party>();
        }

        public Party Get(int id)
        {
            return Entities.FirstOrDefault(party => party.Id == id);
        }

        public Party GetByName(string name)
        {
            return Entities.FirstOrDefault(party => party.Name == name);
        }

        public List<Party> GetAll()
        {
            return Entities.ToList();
        }

        //public List<Party> GetFututeParties()
        //{
        //    var futureParties = new List<Party>();

        //    var sqlQuery = "SELECT * FROM Parties where Date > GETDATE()";
        //    var reader = ExecuteReader(sqlQuery);
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            futureParties.Add(ReaderToObject(reader));
        //        }
        //    }

        //    reader.Close();
        //    return futureParties;
        //}

        //public List<Party> GetNearestParties(int limit)
        //{
        //    var futureParties = new List<Party>();

        //    var sqlQuery = $"SELECT TOP {limit} * from dbo.Parties WHERE Date > GETDATE() ORDER BY Date Desc";
        //    var reader = ExecuteReader(sqlQuery);
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            futureParties.Add(ReaderToObject(reader));
        //        }
        //    }

        //    reader.Close();
        //    return futureParties;
        //}

        public void Create(Party party)
        {
            Entities.Add(party);
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var party = Get(id);

            Entities.Remove(party);
            Context.SaveChanges();
        }

        public void Edit(Party party)
        {
            Context.Parties.Update(party);
            Context.SaveChanges();
        }
    }
}
