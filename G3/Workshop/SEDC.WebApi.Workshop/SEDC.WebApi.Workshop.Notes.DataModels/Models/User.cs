namespace SEDC.WebApi.Workshop.Notes.DataModels.Models
{
    public class User : BaseEntity
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public IEnumerable<Note> NoteList { get; set; }
    }
}