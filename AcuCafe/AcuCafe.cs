using System;
using AcuCafe.Drinks;

namespace AcuCafe
{
    public class AcuCafe
    {
        public static Drink OrderDrink(string type, bool hasMilk, bool hasSugar)
        {
            // TODO: Can we do this any more "cleanly". This does work tho.
            Type test = Type.GetType("AcuCafe.Drinks." + type);
            Drink drink = (Drink)Activator.CreateInstance(test);

            // Can I not pass hasMilk and hasSugar to constructor of Drink...
            drink.HasMilk = hasMilk;
            drink.HasSugar = hasSugar;

            try
            {
                AcuCafe.Prepare(drink);
            }
            catch (Exception ex)
            {
                // todo: better exception handling
                Console.WriteLine("We are unable to prepare your drink.");
                System.IO.File.WriteAllText(@"c:\Error.txt", ex.ToString());
            }

            return drink;
        }


        public static void Prepare(Drink drink)
        {
            string message = "We are preparing the following drink for you: " + drink.Description;
            if (drink.HasMilk)
                message += "with milk";
            else
                message += "without milk";

            if (drink.HasSugar)
                message += "with sugar";
            else
                message += "without sugar";

            Console.WriteLine(message);
        }
    }
}