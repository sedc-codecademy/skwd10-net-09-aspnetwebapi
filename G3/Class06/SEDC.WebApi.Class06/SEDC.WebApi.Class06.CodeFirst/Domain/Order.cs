using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEDC.WebApi.Class06.CodeFirst.Domain
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        public string? OrderName { get; set; }
        [Required]
        public string? OrderAddress { get; set; }
        [Required]
        public string? OrderCity { get; set; }
        public DateTime? DeliveryDate { get; set; }
        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}