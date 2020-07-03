using System;
using System.Collections.Generic;
using System.Linq;
using AcuCafe.Condiments;
using AcuCafe.Drinks;
using AcuCafe.Services;

namespace AcuCafe
{
    public class AcuCafe
    {
        // Added default values for hasCondiment arguments for backwards-compatability.
        public static Drink OrderDrink(string type, bool hasMilk = false, bool hasSugar = false, bool hasChocolate = false)
        {
            Drink drink = null;
            try
            {
                // Rather than explicitly linking the string to Drink requested, use the typename.
                // Approach like this means we can simply create a new Drink type in AcuCafe.Drinks namespace.
                Type drinkType = Type.GetType("AcuCafe.Drinks." + type);
                drink = (Drink)Activator.CreateInstance(drinkType);
            }
            catch (Exception ex)
            {
                // Throw an error with message back in the case where the type entered doesn't match a drink that is actually served.
                ServiceLocatorWrapper.ServiceLocator.GetConsoleService().WriteLine(string.Format("We do not serve {0}.", type));
                
                // Throw the exception to the caller. This is intended to give the "waiter" the opportunity to handle the error. Perhaps prompting customer for details of a new order.
                throw new Exception("Invalid Drink Type supplied.");
            }

            // Reworked how condiments are included in a drink for ease of extension. Rather than using an ever growing list of flags, concatenate to a list.
            // Now, we no longer have to add a new flag for new condiments.
            // Ideally, I would prefer to have modified the signature of the OrderDrink method to take a list of condiments/names, but maintained for backwards compatability.
            if (hasMilk)
                drink.ExtraCondiments.Add(new Milk());
            if (hasSugar)
                drink.ExtraCondiments.Add(new Sugar());
            if (hasChocolate)
                drink.ExtraCondiments.Add(new Chocolate());

            try
            {
                AcuCafe.Prepare(drink);
            }
            catch (Exception ex)
            {
                // using service locator pattern here to enable dependency injection of the console in order to unit test output.
                ServiceLocatorWrapper.ServiceLocator.GetConsoleService().WriteLine("We are unable to prepare your drink.");
                ServiceLocatorWrapper.ServiceLocator.GetFileService().WriteTextToPath(@"c:\Error.txt", ex.ToString());
                // Throw the exception to the caller. The "Waiter" can then handle it.
                throw ex;
            }

            return drink;
        }

        public static void Prepare(Drink drink)
        {
            AcuCafe.ValidateOrder(drink);
            string message = "We are preparing the following drink for you: " + drink.Description;

            // I have made this generic for each condiment. So no changes should be required here in future whenever new condiments are added.
            // Also makes use of the validation setup to ensure we only mention condiments that are relevant to the drink in question.
            foreach (Type condiment in drink.ValidCondiments.OrderBy(c => c.Name))
            {
                if (drink.ExtraCondiments.Where(ec => ec.GetType().Name == condiment.Name).Any())
                {
                    message += ", with " + condiment.Name;
                }
                else
                {
                    message += ", without " + condiment.Name;
                }
            }

            ServiceLocatorWrapper.ServiceLocator.GetConsoleService().WriteLine(message);
        }

        public static void ValidateOrder(Drink drink)
        {
            List<ICondiment> invalidCondimentsRequested = drink.ExtraCondiments.Where(ec => !drink.ValidCondiments.Select(t => t.Name).Contains(ec.GetType().Name)).ToList();

            if (invalidCondimentsRequested.Count > 0)
            {
                ServiceLocatorWrapper.ServiceLocator.GetConsoleService().WriteLine(string.Format("We're afraid there was an issue with your order; {0} does not allow {1}.", drink.Description, string.Join(",", invalidCondimentsRequested.Select(ic => ic.GetType().Name).ToList())));
                throw new Exception("Order failed validation.");
            }
        }
    }
}