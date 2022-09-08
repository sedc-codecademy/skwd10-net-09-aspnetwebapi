using SEDC.NotesApp.Dtos.Users;


namespace SEDC.NotesApp.Services.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserDto registerUserDto);
        string LoginUser(LoginUserDto loginDto);
    }
}
