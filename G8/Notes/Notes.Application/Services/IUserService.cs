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
        UserModel GetUserByCode(string code);

        void UpdatePasswordByCode(UpdatePasswordModel model, string code);

        UserModel PasswordLogin(UserLoginModel model);
    }
}
