namespace Notes.Domain.Models
{
    public class Tag :
        IEntity
    {
        public int Id { get; set; }

        public string Color { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public Note Note { get; set; }
    }
}
