﻿// <auto-generated />
using ASPNETAPI_G2_L6.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASPNETAPI_G2_L6.Migrations
{
    [DbContext(typeof(NotesDbContext))]
    [Migration("20220818171636_seed")]
    partial class seed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ASPNETAPI_G2_L6.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tag")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Color = "Red",
                            Tag = 1,
                            Text = "Text 123",
                            UserId = -1
                        },
                        new
                        {
                            Id = -2,
                            Color = "Green",
                            Tag = 3,
                            Text = "Text 234",
                            UserId = -1
                        },
                        new
                        {
                            Id = -3,
                            Color = "Yellow",
                            Tag = 5,
                            Text = "Text Office 4444",
                            UserId = -1
                        },
                        new
                        {
                            Id = -4,
                            Color = "Yellow",
                            Tag = 5,
                            Text = "Text Office 4444",
                            UserId = -2
                        });
                });

            modelBuilder.Entity("ASPNETAPI_G2_L6.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            FirstName = "Mihajlo",
                            LastName = "Dimovski",
                            Password = "sedc456",
                            Username = "mikid123"
                        },
                        new
                        {
                            Id = -2,
                            FirstName = "Bojan",
                            LastName = "Damcevski",
                            Password = "sedc456",
                            Username = "bokid123"
                        });
                });

            modelBuilder.Entity("ASPNETAPI_G2_L6.Models.Note", b =>
                {
                    b.HasOne("ASPNETAPI_G2_L6.Models.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ASPNETAPI_G2_L6.Models.User", b =>
                {
                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
