using System;

namespace AcuCafe
{
    public class AcuCafe
    {
        public static Drink OrderDrink(string type, bool hasMilk, bool hasSugar)
        {
            Drink drink = new Drink();
            if (type == "Expresso")
            {
                drink = new Expresso();
            }
            else if (type == "HotTea")
                drink = new Tea();
            else if (type == "IceTea")
                drink = new IceTea();

            try
            {
                drink.HasMilk = hasMilk;
                drink.HasSugar = hasSugar;
                drink.Prepare(type);
            }
            catch (Exception ex)
            {
                Console.WriteLine("We are unable to prepare your drink.");
                System.IO.File.WriteAllText(@"c:\Error.txt", ex.ToString());
            }

            return drink;
        }
    }

    public class Drink
    {
        public const double MilkCost = 0.5;
        public const double SugarCost = 0.5;

        public bool HasMilk { get; set; }

        public bool HasSugar { get; set; }
        public string Description { get; }

        public double Cost()
        {
            return 0;
        }

        public void Prepare(string drink)
        {
            string message = "We are preparing the following drink for you: " + Description;
            if (HasMilk)
                message += "with milk";
            else
                message += "without milk";

            if (HasSugar)
                message += "with sugar";
            else
                message += "without sugar";

            Console.WriteLine(message);
        }
    }

    public class Expresso : Drink
    {
        public new string Description
        {
            get { return "Expresso"; }
        }

        public new double Cost()
        {
            double cost = 1.8;

            if (HasMilk)
                cost += MilkCost;

            if (HasSugar)
                cost += SugarCost;

            return cost;
        }
    }

    public class Tea : Drink
    {
        public new string Description
        {
            get { return "Hot tea"; }
        }

        public new double Cost()
        {
            double cost = 1;

            if (HasMilk)
                cost += MilkCost;

            if (HasSugar)
                cost += SugarCost;

            return cost;
        }
    }

    public class IceTea : Drink
    {
        public new string Description
        {
            get { return "Ice tea"; }
        }

        public new double Cost()
        {
            double cost = 1.5;

            if (HasMilk)
                cost += MilkCost;

            if (HasSugar)
                cost += SugarCost;

            return cost;
        }
    }
}