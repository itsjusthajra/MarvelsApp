using MarvelsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MarvelsApp.Services
{
    public class ApplicationDbContext: DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamCharacter> TeamCharacters { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamCharacter>()
                .HasKey(tc => new { tc.TeamId, tc.CharacterId });

            modelBuilder.Entity<TeamCharacter>()
                .HasOne(tc => tc.Team)
                .WithMany(t => t.TeamCharacters)
                .HasForeignKey(tc => tc.TeamId);

            modelBuilder.Entity<TeamCharacter>()
                .HasOne(tc => tc.Character)
                .WithMany() // No navigation in Character model
                .HasForeignKey(tc => tc.CharacterId);
        }


    }
}
