using SEDC.WebApi.Workshop.Notes.DataAccess;
using SEDC.WebApi.Workshop.Notes.DataModels.Models;
using SEDC.WebApi.Workshop.Notes.ServiceModels.UserModels;
using SEDC.WebApi.Workshop.Notes.Sevices.Interfaces;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using SEDC.WebApi.Workshop.Notes.Common.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using SEDC.WebApi.Workshop.Notes.Common.Helpers;
using System.Diagnostics;
using Serilog;
using SEDC.WebApi.Workshop.Notes.Common.Exceptions;

namespace SEDC.WebApi.Workshop.Notes.Sevices
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly string _secret;

        public UserService(IRepository<User> userRepository,
            IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _secret = options.Value.Secret;
        }

        public void Register(RegisterUser request)
        {
            User user = null;
            try
            {
                 user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Username.Equals(request.Username,
                        StringComparison.InvariantCultureIgnoreCase));

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new UserException(null, request.FirstName, ex.Message);
            }


            if(user != null)
            {
                throw new UserException(user.Id, 
                    $"{user.FirstName} {user.LastName}",
                    "User already exists");
            }

            if (!IsValidPassword(request.Password))
            {
                throw new UserException(null, 
                    request.Username, 
                    "Password is not valid");
            }

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Password = HashPassword(request.Password)
            };

            try
            {
                _userRepository.Insert(newUser);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new UserException(null, 
                    request.Username,
                    "Error connecting to Database");
            }
            Log.Information($"User created with username {request.Username}");
        }

        public UserLoginDto Login(LoginModel request)
        {
            var sw = new Stopwatch();
            sw.Start();
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Username.Equals(request.Username,
                                StringComparison.InvariantCultureIgnoreCase));

            if(user == null)
            {
                throw new UserException(null,null, 
                    "User with that username does not exists");
            }

            var hashedPassword = HashPassword(request.Password);
            if(user.Password != hashedPassword)
            {
                Log.Warning($"User {request.Username} tried to log in with wrong password");
                throw new UserException(user.Id, 
                    user.Username, 
                    "Password is not valid");
            }
            sw.Stop();
            Log.Debug($"User validation ended in {sw.ElapsedMilliseconds}");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var login = new UserLoginDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenHandler.WriteToken(token)
            };
            return login;
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
