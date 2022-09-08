namespace SEDC.WebApi.Workshop.Notes.DataModels.Models
{
    public class Note : BaseEntity
    {
        public string? Text { get; set; }
        public string? Color { get; set; }
        public int? Tag { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
