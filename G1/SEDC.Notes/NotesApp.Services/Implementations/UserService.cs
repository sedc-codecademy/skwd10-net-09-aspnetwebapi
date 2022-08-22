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

namespace NotesApp.Services.Implementations
{
    //System.IdentityModel.Tokens.Jwt
    public class UserService : IUserService
    {
        private readonly IRepository<UserDto> _userRepository;

        public UserService(IRepository<UserDto> userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel Authenticate(string username, string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            var hashedPassord = Encoding.ASCII.GetString(md5Data);

            var user = _userRepository.GetAll().SingleOrDefault(user => 
                user.Username == username && user.Password == hashedPassord);

            if (user == null) 
            {
                //TODO: implement validation
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("3t6w9z$C&F)H@McQ");

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
            var md5 = new MD5CryptoServiceProvider();
            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(model.Password));
            var hashedPassord = Encoding.ASCII.GetString(md5Data);

            var user = new UserDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Password = hashedPassord
            };

            _userRepository.Add(user);
        }

        public List<UserModel> GetAllUsers() 
        {
            return _userRepository.GetAll()
                                  .Select(user => UserMapper.ToUserModel(user))
                                  .ToList();
        }
 
    }
}
