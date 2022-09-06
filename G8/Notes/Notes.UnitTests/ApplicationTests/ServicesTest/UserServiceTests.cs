using HashidsNet;
using Moq;
using Notes.Application;
using Notes.Application.Exceptions;
using Notes.Application.Models;
using Notes.Application.Repositories;
using Notes.Application.Services;
using Notes.Application.Services.Implementation;
using Notes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.UnitTests.ApplicationTests.ServicesTest
{
    [TestClass]
    public class UserServiceTests
    {
        private readonly Mock<IRepository<User>> repo = new Mock<IRepository<User>>();
        private readonly Mock<IPasswordHasher> passHasher = new Mock<IPasswordHasher>();
        private readonly Mock<IEmailSender> sender = new Mock<IEmailSender>();
        private readonly Mock<IHashids> hashIds = new Mock<IHashids>();
        private readonly UserService service = null;

        public UserServiceTests()
        {
            service = new UserService(repo.Object, passHasher.Object, sender.Object, hashIds.Object);
        }
        [TestMethod]
        public void GetUser_ById_ReturnsUserModel()
        {
            //Arange
            var userId = 1;
            var user = new User()
            {
                Id = userId
            };
            repo.Setup(x => x.GetById(It.IsAny<int>())).Returns(user);

            // Act
            var model = service.GetUser(userId);

            //Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(user.Id, model.Id);
        }

        [TestMethod]
        public void GetUser_ForNonExistingId_ThrowsNotFoundException()
        {
            //Arange
            var userId = 2;
            var user = new User()
            {
                Id = userId
            };
            repo.Setup(x => x.GetById(1)).Returns(user);

            //Act & Assert
            Assert.ThrowsException<NotFoundException>(() => service.GetUser(userId));
            //try
            //{
            //    // Act
            //    var model = service.GetUser(userId);
            //}
            //catch (NotFoundException)
            //{
            //    // Assert
            //    Assert.IsTrue(true);
            //}
        }

        [TestMethod]
        public void CreateUser_CreatesUser()
        {
            //Arange
            var model = new CreateUserModel();
            var hashedPassword = "Hashed Password";
            passHasher.Setup(x => x.HashPassword(It.IsAny<string>())).Returns(hashedPassword);
            //Act
            var userModel = service.CreateUser(model);
            Assert.IsNotNull(userModel);
        }

        [TestMethod]
        public void ChangePassword_WithSamePassword_ThrowsValidationException()
        {
            //Arrange
            var model = new ChangePasswordModel()
            {
                Password = "OvaEPass",
                OldPassword = "OvaEPass"
            };
            var user = new User();
            var hashPassword = "asdasd";
            passHasher.Setup(x => x.HashPassword(It.IsAny<string>())).Returns(hashPassword);

            //Act
            Assert.ThrowsException<ValidationException>(() => service.ChangePassword(model, 1));
        }

        [TestMethod]
        public void ChangePassword_WithNewPassword_ChangesPassword()
        {
            //Arrange
            var model = new ChangePasswordModel
            {
                Password = "1",
                OldPassword = "2"
            };
            var user = new User();
            var userId = 1;
            repo.Setup(x => x.GetById(userId)).Returns(user);
            var oldHashedPasswrod = "123";
            user.Password = oldHashedPasswrod;
            passHasher.Setup(x => x.HashPassword(model.OldPassword)).Returns(oldHashedPasswrod);
            var hashedPasswrod = "12";
            passHasher.Setup(x => x.HashPassword(model.Password)).Returns(hashedPasswrod);

            // Act
            service.ChangePassword(model, userId);

            //Assert
            Assert.AreEqual(hashedPasswrod, user.Password);
        }

        [TestMethod]
        public void ChangePassword_WithWrongPassword_ThrowsValidationException()
        {
            //Arrange

            var model = new ChangePasswordModel
            {
                OldPassword = "Old",
                Password = "New"
            };
            var userId = 1;

            var user = new User
            {
                Password = "asd"
            };
            repo.Setup(x => x.GetById(userId)).Returns(user);

            var hashPassword = "asdasda";
            passHasher.Setup(x => x.HashPassword(model.OldPassword)).Returns(hashPassword);
            // Act & Assert
            Assert.ThrowsException<ValidationException>(() => service.ChangePassword(model, userId));
        }
    }
}
