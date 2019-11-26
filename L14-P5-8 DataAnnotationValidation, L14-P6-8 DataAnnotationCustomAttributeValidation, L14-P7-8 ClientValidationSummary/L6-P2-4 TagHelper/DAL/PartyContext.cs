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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var party = modelBuilder.Entity<Party>();
            party.Property(c => c.Name).IsRequired().IsUnicode().HasMaxLength(200);
            party.Property(c => c.Location).IsRequired().IsUnicode().HasMaxLength(200);
            party.Property(c => c.Date).IsRequired();

            var participant = modelBuilder.Entity<Participant>();
            participant.Property(c => c.Name).IsRequired().IsUnicode().HasMaxLength(200);
            participant.Property(c => c.IsAttend).IsRequired();
            participant.Property(c => c.Reason).IsUnicode().HasMaxLength(200);
            participant.Property(c => c.Avatar).IsUnicode().HasMaxLength(200);

            //modelBuilder.Entity<Participant>()
            //    .HasOne(s => s.Party)
            //    .WithMany(g => g.Participants)
            //    .HasForeignKey(s => s.PartyId);
        }
    }
}
