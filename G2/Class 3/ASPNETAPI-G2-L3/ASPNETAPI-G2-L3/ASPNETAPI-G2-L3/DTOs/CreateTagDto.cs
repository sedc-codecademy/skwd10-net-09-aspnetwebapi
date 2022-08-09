namespace ASPNETAPI_G2_L3.DTOs
{
    public class CreateTagDto
    {
        public int Id { get; set; }

        public int NoteId { get; set; }

        public string Color { get; set; }

        public string Name { get; set; }
    }
}
