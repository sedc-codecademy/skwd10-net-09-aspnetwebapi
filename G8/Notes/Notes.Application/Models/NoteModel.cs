namespace Notes.Application.Models
{
    public class NoteModel
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public string? Tag { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;
    }
}
