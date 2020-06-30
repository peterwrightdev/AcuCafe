using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcuCafe;

namespace AcuCafeTest
{
    [TestClass]
    public class AcuCafeTestClass
    {
        [DataTestMethod]
        [DataRow("Expresso", 0, 0)]
        public void OrderDrinkTest(string type, int hasMilk, int hasSugar)
        {
            // Arrange

            // Act
            AcuCafe.AcuCafe.OrderDrink(type, hasMilk == 1, hasSugar == 1);

            // Assert

        }
    }
}
