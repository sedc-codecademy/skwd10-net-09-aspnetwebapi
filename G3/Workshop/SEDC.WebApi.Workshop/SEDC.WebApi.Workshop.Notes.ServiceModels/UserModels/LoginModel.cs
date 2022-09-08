using System.ComponentModel.DataAnnotations;

namespace SEDC.WebApi.Workshop.Notes.ServiceModels.UserModels
{
    public class LoginModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
