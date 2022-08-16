using System.ComponentModel.DataAnnotations;

namespace SEDC.WebApi.Class03._1.App.Models
{
    public class SearchInput
    {
        [Required]
        public int? Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
