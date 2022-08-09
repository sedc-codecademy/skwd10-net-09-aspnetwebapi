namespace ASPNETAPI_G2_L3.Entities
{
    public class Note : BaseEntity
    {
        public string Text { get; set; }

        public string Color { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();

        public User User { get; set; }
    }
}
