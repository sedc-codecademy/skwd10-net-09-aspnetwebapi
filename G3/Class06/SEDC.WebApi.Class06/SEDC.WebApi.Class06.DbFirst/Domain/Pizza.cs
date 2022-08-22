using System;
using System.Collections.Generic;

namespace SEDC.WebApi.Class06.DbFirst.Domain
{
    public partial class Pizza
    {
        public Pizza()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int PizzaTypeId { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }

        public virtual PizzaType PizzaType { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
