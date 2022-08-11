using Notes.Application.Models;

namespace Notes.Application.Services
{
    public interface IUserService
    {
        public UserModel GetUser(int id);

        public IEnumerable<UserModel> GetUsers();

        public UserModel CreateUser(CreateUserModel model);

        public void ChangePassword(ChangePasswordModel model, int id);

        public void ForgotPassword(ForgotPasswordModel model, string url);
    }
}
