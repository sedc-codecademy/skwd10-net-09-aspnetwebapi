using System;
using System.Collections.Generic;

namespace SEDC.WebApi.Class06.DbFirst.Domain
{
    public partial class Employee
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public DateTime HireDate { get; set; }
        public string? City { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
