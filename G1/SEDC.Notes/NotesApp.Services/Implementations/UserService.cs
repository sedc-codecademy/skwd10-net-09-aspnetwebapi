using NotesApp.DAL;
using NotesApp.DAL.Repositories;
using NotesApp.DataModels;
using NotesApp.Mapper;
using NotesApp.Services.Interfaces;
using SEDC.Notes.InerfaceModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using NotesApp.Helpers;
using NotesApp.Configurations;
using Microsoft.Extensions.Options;
using NotesApp.Exceptions;
using System.Text.RegularExpressions;

namespace NotesApp.Services.Implementations
{
    //System.IdentityModel.Tokens.Jwt
    public class UserService : IUserService
    {
        private readonly IRepository<UserDto> _userRepository;
        private readonly AppSettings _appSettings;

        public UserService(IRepository<UserDto> userRepository, 
                           IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _appSettings = options.Value;
        }

        public UserModel Authenticate(string username, string password)
        {

            var hashedPassword = StringHasher.HashGenerator(password);

            var user = _userRepository.GetAll().SingleOrDefault(user => 
                user.Username == username && user.Password == hashedPassword);

            if (user == null) 
            {
                throw new UserException(null, null, "This user does not exists in the database!");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptior = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[] 
                    {
                        new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }               
                ),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptior);

            return new UserModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = tokenHandler.WriteToken(token)
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

            var hashedPassword = StringHasher.HashGenerator(model.Password);

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
