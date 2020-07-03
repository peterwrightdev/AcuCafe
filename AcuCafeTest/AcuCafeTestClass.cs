using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AcuCafe;
using AcuCafe.Services;
using System.Reflection.Metadata;
using AcuCafe.Drinks;

namespace AcuCafeTest
{
    [TestClass]
    public class AcuCafeTestClass
    {
        // The following unit tests cover all possible inputs with a valid type of Drink supplied.
        [DataTestMethod]
        [DataRow("Expresso", 0, 0, 0, 1.8, null, "We are preparing the following drink for you: Expresso, without Chocolate, without Milk, without Sugar")]
        [DataRow("Expresso", 1, 0, 0, 2.5, null, "We are preparing the following drink for you: Expresso, with Chocolate, without Milk, without Sugar")]
        [DataRow("Expresso", 0, 1, 0, 2.3, null, "We are preparing the following drink for you: Expresso, without Chocolate, with Milk, without Sugar")]
        [DataRow("Expresso", 0, 0, 1, 2.3, null, "We are preparing the following drink for you: Expresso, without Chocolate, without Milk, with Sugar")]
        [DataRow("Expresso", 1, 1, 0, 3, null, "We are preparing the following drink for you: Expresso, with Chocolate, with Milk, without Sugar")]
        [DataRow("Expresso", 1, 0, 1, 3, null, "We are preparing the following drink for you: Expresso, with Chocolate, without Milk, with Sugar")]
        [DataRow("Expresso", 0, 1, 1, 2.8, null, "We are preparing the following drink for you: Expresso, without Chocolate, with Milk, with Sugar")]
        [DataRow("Expresso", 1, 1, 1, 3.5, null, "We are preparing the following drink for you: Expresso, with Chocolate, with Milk, with Sugar")]
        [DataRow("HotTea", 0, 0, 0, 1, null, "We are preparing the following drink for you: Hot tea, without Milk, without Sugar")]
        [DataRow("HotTea", 1, 0, 0, 0, "System.Exception: Order failed validation.", "We're afraid there was an issue with your order; Hot tea does not allow Chocolate.")]
        [DataRow("HotTea", 0, 1, 0, 1.5, null, "We are preparing the following drink for you: Hot tea, with Milk, without Sugar")]
        [DataRow("HotTea", 0, 0, 1, 1.5, null, "We are preparing the following drink for you: Hot tea, without Milk, with Sugar")]
        [DataRow("HotTea", 1, 1, 0, 0, "System.Exception: Order failed validation.", "We're afraid there was an issue with your order; Hot tea does not allow Chocolate.")]
        [DataRow("HotTea", 1, 0, 1, 0, "System.Exception: Order failed validation.", "We're afraid there was an issue with your order; Hot tea does not allow Chocolate.")]
        [DataRow("HotTea", 0, 1, 1, 2, null, "We are preparing the following drink for you: Hot tea, with Milk, with Sugar")]
        [DataRow("HotTea", 1, 1, 1, 2, "System.Exception: Order failed validation.", "We're afraid there was an issue with your order; Hot tea does not allow Chocolate.")]
        [DataRow("IceTea", 0, 0, 0, 1.5, null, "We are preparing the following drink for you: Ice tea, without Sugar")]
        [DataRow("IceTea", 1, 0, 0, 0, "System.Exception: Order failed validation.", "We're afraid there was an issue with your order; Ice tea does not allow Chocolate.")]
        [DataRow("IceTea", 0, 1, 0, 0, "System.Exception: Order failed validation.", "We're afraid there was an issue with your order; Ice tea does not allow Milk.")]
        [DataRow("IceTea", 0, 0, 1, 2, null, "We are preparing the following drink for you: Ice tea, with Sugar")]
        [DataRow("IceTea", 1, 1, 0, 0, "System.Exception: Order failed validation.", "We're afraid there was an issue with your order; Ice tea does not allow Milk,Chocolate.")]
        [DataRow("IceTea", 1, 0, 1, 0, "System.Exception: Order failed validation.", "We're afraid there was an issue with your order; Ice tea does not allow Chocolate.")]
        [DataRow("IceTea", 0, 1, 1, 0, "System.Exception: Order failed validation.", "We're afraid there was an issue with your order; Ice tea does not allow Milk.")]
        [DataRow("IceTea", 1, 1, 1, 0, "System.Exception: Order failed validation.", "We're afraid there was an issue with your order; Ice tea does not allow Milk,Chocolate.")]
        public void OrderDrinkTest(string type, int hasChocolate, int hasMilk, int hasSugar, double expectedCost, string errorMessage, string consoleMessage)
        {
            // Arrange
            Mock<IServiceLocator> moqServiceLocator = new Mock<IServiceLocator>();
            Mock<IConsoleService> moqConsoleService = new Mock<IConsoleService>();
            Mock<IFileSystemService> moqFileSystemService = new Mock<IFileSystemService>();

            moqServiceLocator.Setup(msl => msl.GetConsoleService()).Returns(moqConsoleService.Object);
            moqServiceLocator.Setup(msl => msl.GetFileService()).Returns(moqFileSystemService.Object);

            ServiceLocatorWrapper.ServiceLocator = moqServiceLocator.Object;

            // Act
            Drink result = null;
            try
            {
                result = AcuCafe.AcuCafe.OrderDrink(type, hasMilk == 1, hasSugar == 1, hasChocolate == 1);
            }
            catch { }
            finally
            {
                // Assert
                if (errorMessage != null)
                {
                    moqConsoleService.Verify(mcs => mcs.WriteLine(It.Is<string>(s => s.Equals("We are unable to prepare your drink."))), Times.Once);
                    moqConsoleService.Verify(mcs => mcs.WriteLine(It.Is<string>(s => s.Equals(consoleMessage))), Times.Once);
                    moqFileSystemService.Verify(mfs => mfs.WriteTextToPath(It.IsAny<string>(), It.Is<string>(s => s.Contains(errorMessage))), Times.Once);
                }
                else
                {
                    moqConsoleService.Verify(mcs => mcs.WriteLine(It.Is<string>(s => s.Equals("We are unable to prepare your drink."))), Times.Never);
                    moqConsoleService.Verify(mcs => mcs.WriteLine(It.Is<string>(s => s.Equals(consoleMessage))), Times.Once);

                    // Verify cost of drink
                    Assert.AreEqual(expectedCost, result.Cost());
                }
            }
        }

        [TestMethod]
        public void OrderNonExistantDrinkTest()
        {
            // Arrange
            Mock<IServiceLocator> moqServiceLocator = new Mock<IServiceLocator>();
            Mock<IConsoleService> moqConsoleService = new Mock<IConsoleService>();
            Mock<IFileSystemService> moqFileSystemService = new Mock<IFileSystemService>();

            moqServiceLocator.Setup(msl => msl.GetConsoleService()).Returns(moqConsoleService.Object);
            moqServiceLocator.Setup(msl => msl.GetFileService()).Returns(moqFileSystemService.Object);

            ServiceLocatorWrapper.ServiceLocator = moqServiceLocator.Object;

            try
            {
                AcuCafe.AcuCafe.OrderDrink("Pizza");
            }
            catch { }
            finally
            {
                moqConsoleService.Verify(mcs => mcs.WriteLine(It.Is<string>(s => s.Equals("We do not serve Pizza."))), Times.Once);
            }


        }
    }
}
