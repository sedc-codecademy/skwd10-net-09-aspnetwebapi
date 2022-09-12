using NotesApp.DAL;
using NotesApp.DataModels;
using NotesApp.Mapper;
using NotesApp.Services.Interfaces;
using NotesApp.Helpers;
using NotesApp.Configurations;
using Microsoft.Extensions.Options;
using NotesApp.Exceptions;
using System.Text.RegularExpressions;
using NotesApp.InerfaceModels.Models;

namespace NotesApp.Services.Implementations
{
    //System.IdentityModel.Tokens.Jwt
    public class UserService : IUserService
    {
        private readonly IRepository<UserDto> _userRepository;
        private readonly IStringHasher hasher;
        private readonly ISecurityHandler handler;
        private readonly AppSettings _appSettings;

        public UserService(IRepository<UserDto> userRepository, 
                           IOptions<AppSettings> options,
                           IStringHasher hasher,
                           ISecurityHandler handler)
        {
            _userRepository = userRepository;
            this.hasher = hasher;
            this.handler = handler;
            _appSettings = options.Value;
        }

        public UserModel Authenticate(string username, string password)
        {
            var hashedPassword = hasher.HashGenerator(password);

            var user = _userRepository.GetAll().SingleOrDefault(user =>
                user.Username == username && user.Password == hashedPassword);

            if (user == null)
            {
                throw new UserException(null, null, "This user does not exists in the database!");
            }

            string token = handler.GenerateSecurityToken(user);

            return new UserModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = token
            };
        }



        public void Register(RegisterModel model) 
        {
            //throw new Exception("Fatal Error");

            if (string.IsNullOrEmpty(model.FirstName)) 
            {
                throw new UserException(null, model.Username, "First name is required!");
            }

            if (string.IsNullOrEmpty(model.LastName))
            {
                throw new UserException(null, model.Username, "Last name is required!");
            }

            if (string.IsNullOrEmpty(model.Username))
            {
                throw new UserException(null, model.Username, "Usernamew is required!");
            }

            if (ValidateUsername(model.Username))
            {
                throw new UserException(null, model.Username, "Username alreay exsits!");
            }

            if (!ValidatePassword(model.Password)) 
            {
                throw new UserException(null, model.Username, "Password is too weak!");
            }

            if (model.Password != model.ConfirmPassword) 
            {
                throw new UserException(null, model.Username, "Password did not match!");
            }

            var hashedPassword = hasher.HashGenerator(model.Password);

            var user = new UserDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Password = hashedPassword
            };

            _userRepository.Add(user);
        }

        public List<UserModel> GetAllUsers() 
        {
            return _userRepository.GetAll()
                                  .Select(user => UserMapper.ToUserModel(user))
                                  .ToList();
        }


        private bool ValidateUsername(string username) 
        {
            return _userRepository.GetAll().Any(user => user.Username == username);
        }

        private bool ValidatePassword(string password) 
        {
            var passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            var match = passwordRegex.Match(password);
            return match.Success;
        }
 
    }
}
