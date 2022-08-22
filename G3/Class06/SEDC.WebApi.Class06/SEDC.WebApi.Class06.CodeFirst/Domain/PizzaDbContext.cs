using Microsoft.EntityFrameworkCore;

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
    }
}
