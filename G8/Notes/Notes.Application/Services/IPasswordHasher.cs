namespace Notes.Application.Services
{
    public interface IPasswordHasher
    {
        public string HashPassword(string password);
    }
}
