using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entities;
using Notes.Domain.Enums;

namespace Notes.Storage.Database
{
    public class NotesDbContext : DbContext, INotesDbContext
    {
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>()
                .HasMany(x => x.Tags)
                .WithOne(x => x.Note)
                .HasForeignKey(x => x.NoteId);

            modelBuilder.Entity<Tag>()
                .HasOne(x => x.Note)
                .WithMany(x => x.Tags)
                .HasForeignKey(x => x.NoteId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Notes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<User>()
                .HasData(
                new User()
                {
                    Id = 1,
                    FirstName = "Bojan",
                    LastName = "Damchevski",
                    Role = Role.Admin,
                    Password = "Test123!",
                    Username = "Bojandamcevski98"
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Mihajlo",
                    LastName = "Dimovski",
                    Role = Role.Admin,
                    Password = "Test123!",
                    Username = "MihajloDimovski96"
                });

            modelBuilder.Entity<Note>()
                .HasData(
                new Note()
                {
                    Id = 1,
                    Name = "Note1",
                    Description = "Some description",
                    UserId = 1
                },
                new Note()
                {
                    Id = 2,
                    Name = "Note2",
                    Description = "Some description",
                    UserId = 2
                },
                new Note()
                {
                    Id = 3,
                    Name = "Note3",
                    Description = "Some description",
                    UserId = 1
                });

            modelBuilder.Entity<Tag>()
                .HasData(
                new Tag()
                {
                    Id = 1,
                    Name = "Tag1",
                    Color = "Green",
                    NoteId = 1
                },
                new Tag()
                {
                    Id = 2,
                    Name = "Tag2",
                    Color = "Yellow",
                    NoteId = 2
                });
        }
    }
}
