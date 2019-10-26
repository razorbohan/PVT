using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using L6_P2_4_TagHelper.Infrastructure;
using Newtonsoft.Json;
using L6_P2_4_TagHelper.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace L6_P2_4_TagHelper.DAL
{
    public interface IParticipantsRepository
    {
        Participant Get(int id);
        IQueryable<Participant> GetAll();
        void Create(Participant participant);
        void Delete(int id);
        void Edit(Participant participant);
    }

    public class ParticipantsRepository : IParticipantsRepository
    {
        private PartyContext Context { get; }
        private DbSet<Participant> Entities { get; }

        public ParticipantsRepository(PartyContext context)
        {
            Context = context;
            Entities = context.Set<Participant>();
        }

        public Participant Get(int id)
        {
            return Entities.FirstOrDefault(participant => participant.Id == id);
        }

        public IQueryable<Participant> GetAll()
        {
            return Entities;
        }

        public void Create(Participant participant)
        {
            Entities.Add(participant);
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var participant = Get(id);

            Entities.Remove(participant);
            Context.SaveChanges();
        }

        public void Edit(Participant participant)
        {
            Context.Participants.Update(participant);
            Context.SaveChanges();
        }
    }
}
