using HashidsNet;
using Notes.Application.Exceptions;
using Notes.Application.Mapper;
using Notes.Application.Models;
using Notes.Application.Repositories;
using Notes.Domain.Models;

namespace Notes.Application.Services.Implementation
{
    public class UserService
        : IUserService
    {
        private readonly IRepository<User> repository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IEmailSender emailSender;
        private readonly IHashids hashids;

        public UserService(IRepository<User> repository, IPasswordHasher hasher, IEmailSender emailSender, IHashids hashids)
        {
            this.repository = repository;
            this.passwordHasher = hasher;
            this.emailSender = emailSender;
            this.hashids = hashids;
        }

        public UserModel GetUser(int id)
        {
            var user = repository.GetById(id);
            if(user == null)
            {
                throw new NotFoundException("User not found");
            }
            return user.ToModel();
        }

        public IEnumerable<UserModel> GetUsers()
        {
            // LINQ 
            var test = new List<string>();
            test.Where(x => x != "Jovan");
            IEnumerable<UserModel> result = repository.GetAll()
                .Where(x => x.ForgotPasswordCode != null)
                .OrderBy(x => x.Username)
                .ToList()
                .Select(x => x.ToModel());

            return result.ToList();
        }

        public UserModel CreateUser(CreateUserModel model)
        {
            var password = passwordHasher.HashPassword(model.Password);
            var user = new User(model.UserName, password, model.Name, model.LastName, model.Email);
            repository.Create(user);
            return user.ToModel();
        }

        public void ChangePassword(ChangePasswordModel model, int id)
        {
            if(model.OldPassword == model.Password)
            {
                throw new ValidationException("Old password can not be the same as the new password");
            }

            var user = repository.GetById(id) ?? throw new NotFoundException("User doesn't exist");

            if(user.Password != passwordHasher.HashPassword(model.OldPassword))
            {
                throw new ValidationException("Old password is wrong");
            }

            user.Password = passwordHasher.HashPassword(model.Password);
            repository.Update(user);
        }

        //url https://localhost:[port]
        public void ForgotPassword(ForgotPasswordModel model, string url)
        {
            var user = repository.GetAll().FirstOrDefault(x => x.Email == model.Email);
            if(user == null)
            {
                throw new NotFoundException("User doesn't exist");
            }
            var code = hashids.Encode(user.Id);
            // send email with code
            var body = $"You can reset your password <a href='{url}/api/v1/user/code/{code}'>here</a>";
            var subject = "Reset password";
            emailSender.SendMail(user.Email, body, subject);
            user.SetForgotPasswordCode(code);
            repository.Update(user);
        }

        public UserModel GetUserByCode(string code)
        {
            var id = hashids.DecodeSingle(code);
            var user = repository.GetById(id);
            if (user == null)
            {
                throw new NotFoundException("User doesn't exist");
            }
            return user.ToModel();
        }

        public void UpdatePasswordByCode(UpdatePasswordModel model, string code)
        {
            var id = hashids.DecodeSingle(code);
            if(model.Id != id)
            {
                throw new ValidationException();
            }
            var user = repository.GetById(id);
            if (user == null)
            {
                throw new NotFoundException("User doesn't exist");
            }
            user.ClearForgotPasswordCode();
            user.Password = passwordHasher.HashPassword(model.Password);
            repository.Update(user);
        }

        public UserModel PasswordLogin(UserLoginModel model)
        {
            var user = repository.GetAll()
                .FirstOrDefault(x => x.Username == model.UsernameOrEmail || x.Email == model.UsernameOrEmail) ??
                throw new Exception("Auth Excetion");

            if(user.Password != passwordHasher.HashPassword(model.Password))
            {
                throw new Exception("Auth Excetion");
            }

            return user.ToModel();
        }
    }
}
