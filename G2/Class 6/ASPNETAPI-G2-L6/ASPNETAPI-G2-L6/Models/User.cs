using System.ComponentModel.DataAnnotations;

namespace ASPNETAPI_G2_L6.Models
{
    public class User : BaseEntity
    {
        //[Required]
        //[MaxLength(255)]
        public string Username { get; set; }

        //[Required]
        //[MaxLength(255)]
        public string FirstName { get; set; }

        //[Required]
        //[MaxLength(255)]
        public string LastName { get; set; }

        //[Required]
        public string Password { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

        public User()
        {
            Notes = new List<Note>();
        }
    }
}
