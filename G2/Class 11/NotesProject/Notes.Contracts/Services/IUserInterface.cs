using Notes.Contracts.DTOs.UserDtos;

namespace Notes.Contracts.Services
{
    public interface IUserService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);

        Task<string> LoginUser(LoginUserDto loginUserDto);
    }
}
