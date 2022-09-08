using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Notes.Tests
{
    [TestClass]
    public class ValuesServiceTests
    {
        private readonly ValuesService _valuesService;

        public ValuesServiceTests()
        {
            _valuesService = new ValuesService();
        }

        [TestMethod]
        public void SumShouldReturnSumOfTwoPositiveNumbers()
        {
            //Arrange
            int num1 = 2;
            int num2 = 5;
            int expectedResult = 7;

            //Act
            int? result = _valuesService.Sum(num1, num2);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void SumShouldReturnNullWhenFirstNumberIsNegative()
        {
            //Arrange
            int num1 = -2;
            int num2 = 5;

            //Act
            int? result = _valuesService.Sum(num1, num2);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SumShouldReturnNullWhenSecondNumberIsNegative()
        {
            //Arrange
            int num1 = 2;
            int num2 = -5;

            //Act
            int? result = _valuesService.Sum(num1, num2);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IsFirstNumLargerShouldReturnTrueWhenFirstNumIsLarger()
        {
            //Arrange
            int num1 = 5;
            int num2 = 2;

            //Act
            bool result = _valuesService.IsFirstNumLarger(num1, num2);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsFirstNumLargerShouldReturnFalseWhenFirstNumIsSmaller()
        {
            //Arrange
            int num1 = 2;
            int num2 = 5;

            //Act
            bool result = _valuesService.IsFirstNumLarger(num1, num2);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetDigitNameShouldReturnNumNameWhenNumIsPassed()
        {
            //Arrange
            int num = 5;
            string expectedResult = "Five";

            //Act
            string result = _valuesService.GetDigitName(num);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GetDigitNameShouldReturnErrorWhenNumAboveTenIsPassed()
        {
            //Arrange
            int num = 13;

            //Act
            Action result = () => _valuesService.GetDigitName(num);

            //Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(result);
            //Assert.ThrowsException<Exception>(result); --> WILL FAIL
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void GetDigitNameShouldReturnErrorWhenNumAboveTenPassed()
        {
            //Arrange
            int num = 13;

            //Act
            _valuesService.GetDigitName(num);
        }
    }
}
