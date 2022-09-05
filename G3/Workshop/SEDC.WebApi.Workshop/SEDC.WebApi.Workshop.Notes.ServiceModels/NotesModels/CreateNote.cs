using System.ComponentModel.DataAnnotations;

namespace SEDC.WebApi.Workshop.Notes.ServiceModels.NotesModels
{
    public class CreateNote
    {
        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }
        [Required]
        [MaxLength(20)]
        public string Color { get; set; }
        public int Tag { get; set; }
    }
}
