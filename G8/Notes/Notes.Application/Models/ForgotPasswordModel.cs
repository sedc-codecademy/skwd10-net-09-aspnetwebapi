using System.ComponentModel.DataAnnotations;

namespace Notes.Application.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
