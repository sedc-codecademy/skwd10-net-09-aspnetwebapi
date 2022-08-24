using Microsoft.EntityFrameworkCore;
using NotesApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using NotesApp.Helpers;

namespace NotesApp.DAL
{
    //Microsoft.EntityFrameworkCore
    //Microsoft.EntityFrameworkCore.Abstractions
    //Microsoft.EntityFrameworkCore.Design
    //Microsoft.EntityFrameworkCore.Relational
    //Microsoft.EntityFrameworkCore.SqlServer
    //Microsoft.EntityFrameworkCore.Tools
    public class NotesAppDbContext : DbContext
    {
        public NotesAppDbContext(DbContextOptions options) : base(options) {}

        public DbSet<UserDto> Users { get; set; }
        public DbSet<NoteDto> Notes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            //configure relations
            modelBuilder.Entity<NoteDto>()
                .HasOne(n => n.User)
                .WithMany(u => u.NoteList)
                .HasForeignKey(n => n.UserId);

            //seeding
            modelBuilder.Entity<UserDto>()
                .HasData(
                    new UserDto
                    {
                        Id = 1,
                        FirstName = "Viktor",
                        LastName = "Jakovlev",
                        Username = "vjakovlev",
                        Password = StringHasher.HashGenerator("P@ssw0rd")
                    });

            modelBuilder.Entity<NoteDto>()
                .HasData(
                    new NoteDto
                    {
                        Id = 1,
                        Color = "Yellow",
                        Text = "Note text 1",
                        Tag = 1,
                        UserId = 1
                    },
                    new NoteDto() 
                    {
                        Id = 2,
                        Color = "Red",
                        Text = "Note text 2",
                        Tag = 3,
                        UserId = 1
                });
        }
     }
}
