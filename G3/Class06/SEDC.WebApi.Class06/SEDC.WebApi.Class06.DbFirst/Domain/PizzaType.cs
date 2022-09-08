using System;
using System.Collections.Generic;

namespace SEDC.WebApi.Class06.DbFirst.Domain
{
    public partial class PizzaType
    {
        public PizzaType()
        {
            Pizzas = new HashSet<Pizza>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}
