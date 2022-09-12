using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SEDC.WebApi.Workshop.Notes.Common.Exceptions;
using SEDC.WebApi.Workshop.Notes.Common.Models;
using SEDC.WebApi.Workshop.Notes.DataAccess;
using SEDC.WebApi.Workshop.Notes.DataModels.Models;
using SEDC.WebApi.Workshop.Notes.ServiceModels.UserModels;
using SEDC.WebApi.Workshop.Notes.Sevices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SEDC.WebApi.Workshop.Notes.Tests
{
    [TestClass]
    public class UserTests
    {
        private Mock<IRepository<User>> _userRepository;
        private IOptions<AppSettings> _options;

        public UserTests()
        {
            _userRepository = new Mock<IRepository<User>>();
            _options = Options.Create<AppSettings>(new AppSettings()
            {
                Secret = "SECRET FOR TESTING"
            });

        }

        [TestMethod]
        public void Register_Username_Exists()
        {
            // Arange
            var users = new List<User>
            {
                new User
                {
                    Id = 3,
                    FirstName = "Trajan",
                    LastName = "Stevkovski",
                    Password = "somePassword",
                    Username = "stevt"
                }
            };

            _userRepository.Setup(x => x.GetAll()).Returns(users);

            var request = new RegisterUser
            {
                Username = "stevt"
            };

            var service = new UserService(_userRepository.Object, _options);

            // Act
            // Assert

            Assert.ThrowsException<UserException>(() =>
            {
                service.Register(request);
            });
            // TODO: add mote asserts
        }

        [TestMethod]
        public void Register_NotValidPassword()
        {
            // Arange
            var users = new List<User>
            {
                new User
                {
                    Id = 3,
                    FirstName = "Trajan",
                    LastName = "Stevkovski",
                    Password = "somePassword",
                    Username = "stevt"
                }
            };

            _userRepository.Setup(x => x.GetAll()).Returns(users);

            var request = new RegisterUser
            {
                Username = "stevt1",
                Password = "ASD123"
            };

            var service = new UserService(_userRepository.Object, _options);

            // Act
            // Assert
            Assert.ThrowsException<UserException>(() =>
            {
                service.Register(request);
            });
            // TODO: add mote asserts
        }

        [TestMethod]
        public void Register_SuccessfulyRegisteredUser()
        {
            // Arange
            var password = "asdzxc123";
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            var users = new List<User>
            {
                new User
                {
                    Id = 3,
                    FirstName = "Trajan",
                    LastName = "Stevkovski",
                    Password = "somePassword",
                    Username = "stevt"
                }
            };

            var request = new RegisterUser
            {
                Username = "stevt1",
                Password = password,
                FirstName = "Trajan1",
                LastName = "Stevkovski1"
            };

            _userRepository.Setup(x => x.Insert(
                It.IsAny<User>())).Callback((User user) =>
                {
                    users.Add(user);
                });

            var expectedUsers = 2;
            

            var service = new UserService(_userRepository.Object, _options);

            // Act
            service.Register(request);

            // Assert
            var expectedUser = users[1];

            Assert.AreEqual(expectedUsers, users.Count);
            Assert.AreEqual(expectedUser.FirstName, request.FirstName);
            Assert.AreEqual(expectedUser.LastName, request.LastName);
            Assert.AreEqual(expectedUser.Username, request.Username);
            Assert.AreNotEqual(expectedUser.Password, request.Password);
            Assert.AreEqual(expectedUser.Password, hashedPassword);
        }

        [TestMethod]
        public void Login_UserNotExists()
        {
            // Arange
            var users = new List<User>
            {
                new User
                {
                    Id = 3,
                    FirstName = "Trajan",
                    LastName = "Stevkovski",
                    Password = "somePassword",
                    Username = "stevt"
                }
            };

            _userRepository.Setup(x => x.GetAll()).Returns(users);

            var request = new LoginModel
            {
                Username = "tstev",
                Password = "Password"
            };

            var service = new UserService(_userRepository.Object, _options);

            // Act
            // Assert
            Assert.ThrowsException<UserException>(() =>
            {
                service.Login(request);
            });
            // TODO: add more assertions
        }

        [TestMethod]
        public void Login_WrongPassword()
        {
            // Arange
            var password = "asdzxc123";
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            var hashedPassword =  Encoding.ASCII.GetString(md5data);

            var users = new List<User>
            {
                new User
                {
                    Id = 3,
                    FirstName = "Trajan",
                    LastName = "Stevkovski",
                    Password = hashedPassword,
                    Username = "stevt"
                }
            };

            _userRepository.Setup(x => x.GetAll()).Returns(users);

            var request = new LoginModel
            {
                Username = "stevt",
                Password = "asd123"
            };

            var service = new UserService(_userRepository.Object, _options);

            // Act
            // Assert
            Assert.ThrowsException<UserException>(() =>
            {
                service.Login(request);
            });
            // TODO: add more assertions
        }

        [TestMethod]
        public void Login_ValidLogin()
        {
            // Arange
            var password = "asdzxc123";
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            var users = new List<User>
            {
                new User
                {
                    Id = 3,
                    FirstName = "Trajan",
                    LastName = "Stevkovski",
                    Password = hashedPassword,
                    Username = "stevt"
                }
            };

            _userRepository.Setup(x => x.GetAll()).Returns(users);

            var request = new LoginModel
            {
                Username = "stevt",
                Password = password
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, $"{users[0].FirstName} {users[0].LastName}"),
                        new Claim(ClaimTypes.NameIdentifier, users[0].Id.ToString())
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var expectedResult = new UserLoginDto
            {
                Id = users[0].Id,
                FirstName = users[0].FirstName,
                LastName = users[0].LastName,
                Token = tokenHandler.WriteToken(token)
            };

            var service = new UserService(_userRepository.Object, _options);

            // Act

            var result = service.Login(request);

            // Assert
            Assert.AreEqual(expectedResult.Id, result.Id);
            Assert.AreEqual(expectedResult.FirstName, result.FirstName);
            Assert.AreEqual(expectedResult.LastName, result.LastName);
            Assert.AreEqual(expectedResult.Token, result.Token);
        }
    }
}
