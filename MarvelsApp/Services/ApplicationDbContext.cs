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
        

    }
}
