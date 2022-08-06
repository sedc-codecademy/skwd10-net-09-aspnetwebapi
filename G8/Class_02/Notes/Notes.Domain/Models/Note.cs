namespace Notes.Domain.Models
{
    public class Note
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public string Tag { get; set; } = string.Empty;

        public User User { get; set; }
    }
}
