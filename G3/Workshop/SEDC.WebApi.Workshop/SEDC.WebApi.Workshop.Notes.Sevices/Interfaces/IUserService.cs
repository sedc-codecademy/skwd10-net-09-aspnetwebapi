using SEDC.WebApi.Workshop.Notes.ServiceModels.UserModels;

namespace SEDC.WebApi.Workshop.Notes.Sevices.Interfaces
{
    public interface IUserService
    {
        void Register(RegisterUser request);
        UserLoginDto Login(LoginModel request);
    }
}
