using Services;

namespace Unit_Testing_Tests
{
    [TestClass]
    public class RandomSeriveTest
    {

        private readonly RandomService _randomService;
        public RandomSeriveTest()
        {
            _randomService = new RandomService();
        }

        // Arrange
        // Act
        // Assert

        // MethodName_StateUnderTest_ExpectedBehavior
        // Sum_BothNumbersAreValid_PositiveResultNumber
        [TestMethod]
        public void Sum_BothNumbersAreValid_PositiveResultNumber()
        {
            // Arrange
            int num1 = 10;
            int num2 = 20;
            int? expectedResult = 30;
            //Act
            int? result = _randomService.Sum(num1, num2);
            //Assert
            Assert.AreEqual(expectedResult, result);    
        }

        [TestMethod]
        public void Sum_LargeNumberIntegers_Null()
        {
            //Arrange
            int num1 = 2111111111;
            int num2 = 2111111111;

            // Act
            int? result = _randomService.Sum(num1, num2);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IsFirstNumberLarger_FirstNumberIsLarger_True()
        {
            //Arrange
            int num1 = 20;
            int num2 = 19;
            //Act
            var result = _randomService.isFirstNumberLarger(num1, num2);
            //Assert
            Assert.IsTrue(result);
        }

        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        //[TestMethod]
        //public void GetDigitName_DoubleDigit_Exception()
        //{
        //    //Arange
        //    int num = 14;
        //    //Act
        //    string result = _randomService.GetDigitName(num);
        //}

        [TestMethod]
        public void GetDigitName_DoubleDigit_Exception()
        {
            //Arange
            int num = 14;
            //Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _randomService.GetDigitName(num));
        }
    }
}