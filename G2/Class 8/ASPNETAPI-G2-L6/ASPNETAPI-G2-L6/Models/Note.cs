namespace ASPNETAPI_G2_L6.Models
{
    public class Note
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Color { get; set; }

        public int Tag { get; set; }

        public int UserId { get; set; }
    }
}
