using System.ComponentModel.DataAnnotations;

namespace SEDC.WebApi.Class06.CodeFirst.Domain
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int PizzaTypeId { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }

        public virtual PizzaType PizzaType { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
