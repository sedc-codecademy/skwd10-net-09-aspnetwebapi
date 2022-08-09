using System.ComponentModel.DataAnnotations;

namespace Notes.Application.Models
{
    public class CreateNoteModel
    {
        [Required]
        public string Text { get; set; } = string.Empty;

        [MaxLength(30)]
        public string Color { get; set; } = string.Empty;

        public string? Tag { get; set; }
    }
}
