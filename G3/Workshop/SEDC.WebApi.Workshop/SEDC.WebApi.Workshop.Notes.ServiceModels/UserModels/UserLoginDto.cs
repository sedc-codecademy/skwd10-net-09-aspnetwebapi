namespace SEDC.WebApi.Workshop.Notes.ServiceModels.UserModels
{
    public class UserLoginDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
    }
}
