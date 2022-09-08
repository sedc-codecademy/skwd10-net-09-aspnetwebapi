using Notes.Contracts.DTOs.UserDtos;

namespace Notes.Contracts.Services
{
    public interface IUserService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);

        Task LoginUser(LoginUserDto loginUserDto);
    }
}
