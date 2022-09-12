using Microsoft.Extensions.Options;
using Moq;
using NotesApp.Configurations;
using NotesApp.DAL;
using NotesApp.DataModels;
using NotesApp.Exceptions;
using NotesApp.Helpers;
using NotesApp.InerfaceModels.Models;
using NotesApp.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Notes.UnitTest.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly Mock<IRepository<UserDto>> repository = new Mock<IRepository<UserDto>>();
        private readonly Mock<IOptions<AppSettings>> settings = new Mock<IOptions<AppSettings>>();
        private readonly Mock<IStringHasher> hasher = new Mock<IStringHasher>();
        private readonly Mock<ISecurityHandler> handler = new Mock<ISecurityHandler>();
        private readonly UserService service;
        public UserServiceTest()
        {
            service = new UserService(repository.Object, settings.Object, hasher.Object, handler.Object);
        }

        [TestMethod]
        public void Authenticate_WithValidUserNameAndPassword_ReturnsJWT()
        {
            //Arrange
            var password = "asdasdasdasd";
            var hashedPassword = "asdasd";
            var username = "username";
            var token = "this is a hashed secure token";
            hasher.Setup(x => x.HashGenerator(It.IsAny<string>())).Returns(hashedPassword);
            var user = new UserDto
            {
                Id = 1,
                Username = username,
                Password = hashedPassword
            };
            repository.Setup(x => x.GetAll()).Returns(new List<UserDto> { user });
            handler.Setup(x => x.GenerateSecurityToken(user)).Returns(token);
            //Act
            var model = service.Authenticate(username, password);
            //Assert
            Assert.AreEqual(user.Id, model.Id);
            Assert.AreEqual(token, model.Token);
        }

        [TestMethod, TestCategory("Register")]
        public void Register_WithoutUsername_ThrowsUserException()
        {
            // Arrange
            RegisterModel model = new RegisterModel();
            // Act & Assert
            Assert.ThrowsException<UserException>(() => service.Register(model));
        }

        [TestMethod, TestCategory("Register")]
        public void Register_NotValidPassword_ThrowsUserException()
        {
            // Arrange
            RegisterModel model = new RegisterModel
            {
                FirstName = "Petko",
                LastName = "Petkovski",
                Username = "Petko petkovski",
                Password = "petko"
            };
            // Act & Assert
            Assert.ThrowsException<UserException>(() => service.Register(model));
        }

        [TestMethod, TestCategory("Register")]
        public void Register_DifferentPasswordFromConfirm_ThrowsUserException()
        {
            // Arrange
            RegisterModel model = new RegisterModel
            {
                FirstName = "Petko",
                LastName = "Petkovski",
                Username = "Petko petkovski",
                Password = "petko123",
                ConfirmPassword = "petko1234"
            };
            // Act & Assert
            Assert.ThrowsException<UserException>(() => service.Register(model));
        }

        // password != confirmPassword ke vrli isklucok
    }
}
