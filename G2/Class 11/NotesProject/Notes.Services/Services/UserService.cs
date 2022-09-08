using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Notes.Contracts.DTOs.UserDtos;
using Notes.Contracts.Services;
using Notes.Domain.Entities;
using Notes.Domain.Repositories;
using Notes.Domain.UnitOfWork;
using Notes.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Notes.Services.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;
        IOptions<Auth> _options;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IOptions<Auth> options)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _options = options;
        }

        public async Task RegisterUser(RegisterUserDto registerUserDto)
        {
            //use MD5 hash algorithm
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            //get the bytes from the password string
            byte[] passwordBytes = Encoding.ASCII.GetBytes(registerUserDto.Password);
            //get the hash
            byte[] passwordHash = mD5CryptoServiceProvider.ComputeHash(passwordBytes);
            //get the string hash
            string hashedPasword = Encoding.ASCII.GetString(passwordHash);

            User newUser = new User()
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Username = registerUserDto.Username,
                Password = hashedPasword,
                Role = (Domain.Enums.Role)registerUserDto.Role
            };

            _userRepository.InsertUser(newUser);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<string> LoginUser(LoginUserDto loginUserDto)
        {
            User userDb = await _userRepository.GetUserByUsernameAsync(loginUserDto.Username);

            if (userDb == null)
            {
                throw new Exception("User not found !!!");
            }

            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] hashedBytes = mD5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(loginUserDto.Password));
            string hashedPassword = Encoding.ASCII.GetString(hashedBytes);

            if (userDb.Password != hashedPassword)
            {
                throw new Exception("Passwords do not match !!!");
            }

            //GENERATE JWT TOKEN

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_options.Value.SecretKey);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(2), // the token will be valid for two hours
                //signature configuration
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                //payload
                Subject = new ClaimsIdentity(
                    new[]
                   {
                        new Claim(ClaimTypes.Name, userDb.Username),
                        new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString()),
                        new Claim("userFullName", $"{userDb.FirstName} {userDb.LastName}"),
                        new Claim(ClaimTypes.Role, userDb.Role.ToString())
                    }
                )
            };
            //generate token with the configuration
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            //convert it to string
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
