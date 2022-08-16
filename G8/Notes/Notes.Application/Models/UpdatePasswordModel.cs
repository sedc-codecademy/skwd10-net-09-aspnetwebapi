using System.ComponentModel.DataAnnotations;

namespace Notes.Application.Models
{
    public class UpdatePasswordModel
    {
        public int Id { get; set; }

        [Compare("ConfirmPassword")]
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
