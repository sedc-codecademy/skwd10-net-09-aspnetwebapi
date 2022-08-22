using Microsoft.EntityFrameworkCore;
using SEDC.WebApi.Class06.CodeFirst.Domain.Mappings;

namespace SEDC.WebApi.Class06.CodeFirst.Domain
{
    public class PizzaDbContext : DbContext
    {
        public PizzaDbContext()
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<PizzaType> PizzaTypes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                "Server=(localdb)\\MSSQLlocaldb;Database=PizzaDBCodeFirst");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Pizza>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<Pizza>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder
                .Entity<Pizza>()
                .HasOne(x => x.PizzaType)
                .WithMany(x => x.Pizzas)
                .HasForeignKey(x => x.PizzaTypeId);

            modelBuilder
                .Entity<PizzaType>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<PizzaType>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder
                .Entity<PizzaType>()
                .Property(x => x.Description)
                .IsRequired(false)
                .HasMaxLength(100);

            modelBuilder
                .Entity<PizzaType>()
                .Property(x => x.Photo)
                .HasMaxLength(100);

            modelBuilder.ApplyConfiguration(new UserMap());

            modelBuilder
                .Entity<User>()
                .HasData(new User()
                {
                    Id = 1,
                    Name = "Trajan",
                    Address = "Temnica",
                    City = "Skopje",
                    Email = "a@b.com"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
