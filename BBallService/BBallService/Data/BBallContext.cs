using Microsoft.EntityFrameworkCore;
using BBallService.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BBallService.Data
{
    public class BBallContext : DbContext
    {
        public BBallContext(DbContextOptions<BBallContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Representation> Representations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Agent>().ToTable("Agent");

            modelBuilder.Entity<Representation>()
                .HasKey(c => new { c.AgentID, c.PlayerID });

            modelBuilder
                .Entity<Player>()
                .Property(p => p.Position)
                .HasConversion(new EnumToStringConverter<Position>());
        }
    }
}
