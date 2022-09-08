using Microsoft.EntityFrameworkCore;
using SEDC.NotesApp.Domain.Models;

namespace SEDC.NotesApp.DataAccess
{
    public class NotesAppDbContext : DbContext
    {
        public NotesAppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //NOTE

            //[Required], [MaxLength(100)]
            modelBuilder.Entity<Note>()
                .Property(x => x.Text)
                .HasMaxLength(100)
                .IsRequired(); //not null

            modelBuilder.Entity<Note>()
                .Property(x => x.Priority)
                .IsRequired();

            modelBuilder.Entity<Note>()
               .Property(x => x.Tag)
               .IsRequired();

            //relation
            modelBuilder.Entity<Note>()
                .HasOne(x => x.User)
                .WithMany(x => x.Notes)
                .HasForeignKey(x => x.UserId);

            //USER
            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(x => x.Username)
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<User>()
               .Property(x => x.Password)
               .IsRequired();

            modelBuilder.Entity<User>()
                .Ignore(x => x.Age);

            //SEED...
        }
    }
}
