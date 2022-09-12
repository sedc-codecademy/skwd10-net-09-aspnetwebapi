using Microsoft.Extensions.Options;
using Moq;
using NotesApp.Configurations;
using NotesApp.DataModels;
using NotesApp.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SEDC.Notes.UnitTest.Helpers
{
    [TestClass]
    public class SecurityHandlerTest
    {
        private readonly Mock<IOptions<AppSettings>> settings = new Mock<IOptions<AppSettings>>();
        public SecurityHandlerTest()
        {
        }

        [TestMethod]
        public void GenerateSecurityToken_WithValidUser_ReturnsNameAndNameIdentifierClaims()
        {
            // Arrange
            string secret = "12345678909876543211231";
            SecurityHandler handler = GetSecurityHandler(secret);
            var user = new UserDto
            {
                Id = 1,
                FirstName = "Petko",
                LastName = "Petkovski"
            };

            //Act
            var result = handler.GenerateSecurityToken(user);

            //Assert
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken? decoded = jwtHandler.ReadJwtToken(result);
            var id = int.Parse(decoded.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value);
            var fullName = decoded.Claims.First(x => x.Type == JwtRegisteredClaimNames.Name).Value;
            Assert.AreEqual(user.Id, id);
            Assert.IsTrue(fullName.Contains(user.FirstName));
            Assert.IsTrue(fullName.Contains(user.LastName));
        }

        private SecurityHandler GetSecurityHandler(string secret)
        {
            var appSettings = new AppSettings
            {
                Secret = secret
            };
            settings.Setup(x => x.Value).Returns(appSettings);
            return new SecurityHandler(settings.Object);
        }
    }
}
