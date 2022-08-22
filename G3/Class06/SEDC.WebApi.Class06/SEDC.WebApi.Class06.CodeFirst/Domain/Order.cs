namespace SEDC.WebApi.Class06.CodeFirst.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderName { get; set; }
        public string? OrderAddress { get; set; }
        public string? OrderCity { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}