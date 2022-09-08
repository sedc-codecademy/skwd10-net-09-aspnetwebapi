using SEDC.DataAnnotations.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEDC.DataAnnotations.Domain.Models
{
    [Table("Notes")] //the corresponding table will be called Notes
    public class Note
    {
        [Key] //PK
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required] //not null
        [MaxLength(100)]
        public string Text { get; set; }
        [Required]
        public Priority Priority { get; set; }
        [Required]
        public Tag Tag { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
