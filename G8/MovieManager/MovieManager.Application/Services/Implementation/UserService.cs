using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieManager.Application.Dto;
using MovieManager.Application.Hasher;
using MovieManager.Application.Mapper;
using MovieManager.Application.Repositories;
using MovieManager.Domain.Exceptions;
using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Services.Implementation
{
    public class UserService : IUserService
    {

        private readonly IRepository<User> _repository;
        private readonly IConfiguration _coniguration;
        private readonly IMapper mapper;

        public UserService(IRepository<User> repository, IMapper mapper, IConfiguration coniguration)
        {
            _repository = repository;
            this.mapper = mapper;
            _coniguration = coniguration;
        }

        public void Register(RegisterDto model)
        {
            model.Password = model.Password.GenerateMD5();
            var user = mapper.Map<User>(model);
            _repository.Create(user);
        }
        public TokenDto Autenticate(LoginDto login)
        {
            User user = _repository.Query().Include(x => x.Movies).Include(x => x.Roles)
                                   .FirstOrDefault(x => x.Username.Equals(login.Username) &&
                                                        x.Password.Equals(login.Password.GenerateMD5())) ??
                                                        throw new NotFoundException("User not found!");

            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_coniguration["Jwt:Key"]));
            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim("CustomClaimTypeUsername", user.Username)
            };


            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var token = new JwtSecurityToken(_coniguration["Jwt:Issuer"],
                                             _coniguration["Jwt:Audience"],
                                             claims,
                                             expires: DateTime.Now.AddMinutes(15),
                                             signingCredentials: credentials);

            TokenDto tokenDto = new TokenDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return tokenDto;
        }

        public List<UserDto> GetUsers()
        {
            var users = _repository.Query().Include(x => x.Roles).Include(x => x.Movies);
            List<UserDto> mappedUsers = users.Select(x => mapper.Map<UserDto>(x)).ToList();
            return mappedUsers;
        }
    }
}
