using AcuCafe.Condiments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AcuCafe.Drinks
{
    public abstract class Drink
    {
        // This property is to be populated by the extending classes to specify which Condiments are valid for a given Drink. This is applied generically in a validation step in AcuCafe.
        public abstract List<Type> ValidCondiments { get; }

        public abstract string Description { get; }

        // Base cost of the drink without including condiment prices.
        protected abstract double BaseCost { get; }

        public double Cost()
        {
            double cost = BaseCost;

            // For each condiment added, add the price of it.
            // This removes the need for us to explicitly add the price for each type of condiment explicitly when new condiments are added.
            foreach(ICondiment condiment in this.ExtraCondiments.Distinct().ToList())
            {
                cost += condiment.Cost;
            }

            return cost;
        }

        public List<ICondiment> ExtraCondiments = new List<ICondiment>();

        // Strictly speaking, these flags are no longer required within AcuCafe. However, I have maintained them with getters only in case a calling application would make use of them.
        public bool HasChocolate { get { return this.ExtraCondiments.Where(c => c.GetType().Name == typeof(Chocolate).Name).Any(); } }
        public bool HasMilk { get { return this.ExtraCondiments.Where(c => c.GetType().Name == typeof(Milk).Name).Any(); } }
        public bool HasSugar { get { return this.ExtraCondiments.Where(c => c.GetType().Name == typeof(Sugar).Name).Any(); } }
    }
}
