using System.ComponentModel.DataAnnotations;

namespace SEDC.WebApi.Workshop.Notes.ServiceModels.UserModels
{
    public class RegisterUser
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
