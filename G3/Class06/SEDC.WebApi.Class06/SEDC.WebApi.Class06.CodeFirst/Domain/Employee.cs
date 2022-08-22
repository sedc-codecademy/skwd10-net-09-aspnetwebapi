namespace SEDC.WebApi.Class06.CodeFirst.Domain
{
    public class Employee
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public DateTime HireDate { get; set; }
        public string? City { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
