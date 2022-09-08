using System.ComponentModel.DataAnnotations;

namespace SEDC.WebApi.Class06.CodeFirst.Domain
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string? LastName { get; set; }
        [MaxLength(100)]
        public string? Title { get; set; }
        public DateTime HireDate { get; set; }
        public string? City { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
