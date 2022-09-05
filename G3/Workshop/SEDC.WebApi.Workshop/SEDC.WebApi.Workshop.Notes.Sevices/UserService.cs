using SEDC.WebApi.Workshop.Notes.DataAccess;
using SEDC.WebApi.Workshop.Notes.DataModels.Models;
using SEDC.WebApi.Workshop.Notes.ServiceModels.UserModels;
using SEDC.WebApi.Workshop.Notes.Sevices.Interfaces;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

namespace SEDC.WebApi.Workshop.Notes.Sevices
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(RegisterUser request)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Username.Equals(request.Username,
                        StringComparison.InvariantCultureIgnoreCase));

            if(user != null)
            {
                throw new Exception("Username already exists");
            }

            if (!IsValidPassword(request.Password))
            {
                throw new Exception("Password is not valid");
            }

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Password = HashPassword(request.Password)
            };

            _userRepository.Insert(newUser);
        }

        public UserLoginDto Login(LoginModel request)
        {
            throw new NotImplementedException();
        }

        private bool IsValidPassword(string password)
        {
            var passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            var match = passwordRegex.Match(password);
            return match.Success;
        }

        private string HashPassword(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            return Encoding.ASCII.GetString(md5data);
        }
    }
}
