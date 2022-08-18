using ASPNETAPI_G2_L6.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNETAPI_G2_L6.Models
{
    public class Note : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Text { get; set; }

        public string Color { get; set; }

        public TagType Tag { get; set; }

        public int UserId { get; set; }

        public Note()
        {

        }
    }
}
