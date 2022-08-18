using ASPNETAPI_G2_L6.Database.Configurations;
using ASPNETAPI_G2_L6.Database.Seeds;
using ASPNETAPI_G2_L6.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETAPI_G2_L6.Database
{
    public class NotesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Note> Notes { get; set; }

        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base (options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotesDbContext).Assembly);
            modelBuilder.Entity<Note>()
                        .HasData(NotesSeed.NOTES);
        }
    }
}
