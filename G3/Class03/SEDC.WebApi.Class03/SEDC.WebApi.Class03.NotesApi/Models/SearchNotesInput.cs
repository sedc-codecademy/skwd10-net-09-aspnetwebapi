using System.ComponentModel.DataAnnotations;

namespace SEDC.WebApi.Class03.NotesApi.Models
{
    public class SearchNotesInput
    {
        public int? Id { get; set; }
        public string? OrderBy { get; set; }
        public string? Color { get; set; }
    }
}
