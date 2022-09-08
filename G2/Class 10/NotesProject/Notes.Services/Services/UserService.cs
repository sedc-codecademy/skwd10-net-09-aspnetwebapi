using Notes.Contracts.DTOs.UserDtos;
using Notes.Contracts.Services;
using Notes.Domain.Entities;
using Notes.Domain.Repositories;
using Notes.Domain.UnitOfWork;
using System.Security.Cryptography;
using System.Text;

namespace Notes.Services.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
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

        public async Task LoginUser(LoginUserDto loginUserDto)
        {
            User userDb = await _userRepository.GetUserByUsernameAsync(loginUserDto.Username);

            if (userDb == null)
            {
                throw new Exception("User not found !!!");
            }

            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] hashedBytes = mD5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(loginUserDto.Password));
            string hashedPassword = Encoding.ASCII.GetString(hashedBytes);

            if (loginUserDto.Password != hashedPassword)
            {
                throw new Exception("Passwords do not match !!!");
            }
        }
    }
}
