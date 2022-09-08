namespace SEDC.WebApi.Class06.CodeFirst.Domain
{
    public class PizzaType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}