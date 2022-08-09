namespace Notes.Domain.Models
{
    public class Note
    {
        public Note(string text, string color, User user)
        {
            Text = text ?? throw new ArgumentNullException(nameof(user));
            Color = color ?? throw new ArgumentNullException(nameof(user));
            User = user ?? throw new ArgumentNullException(nameof(user));
        }
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public string? Tag { get; set; }

        public User User { get; set; }
    }
}
