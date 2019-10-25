using L6_P2_4_TagHelper.Models;
using Microsoft.EntityFrameworkCore;

namespace L6_P2_4_TagHelper.DAL
{
    public class PartyContext : DbContext
    {
        public PartyContext(DbContextOptions<PartyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Party> Parties { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }
    }
}
