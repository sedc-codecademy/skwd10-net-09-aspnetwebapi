using Notes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.UnitTests.DomainTest
{
    [TestClass]
    public class UsersTest
    {

        // MethodName_Scenario_ExpectedResult -> name of unit test
        // 3A -> Arrange -> act -> Assert
        // Simple unit tests
        // No logic
        [TestMethod]
        public void SetForgotPasswordCode_WithCode_SetsForgotPasswordCodeCreatedProperty()
        {
            // Arrange
            var user = new User();
            var code = "code";

            // Act
            user.SetForgotPasswordCode(code);

            //Assert
            Assert.IsNotNull(user.ForgotPasswordCodeCreated);
        }

        [TestMethod]
        public void SetForgotPasswordCode_WithCode_SetsForgotPasswordCodeProperty()
        {
            // Arrange
            var user = new User();
            var code = "code";

            // Act
            user.SetForgotPasswordCode(code);

            //Assert
            Assert.AreEqual(code, user.ForgotPasswordCode);
        }


    }
}
