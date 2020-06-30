using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcuCafe.Drinks
{
    public abstract class Drink
    {
        public abstract string Description { get; }

        protected abstract double innerCost { get; }

        public double Cost()
        {
            double cost = innerCost;

            // Prices can't be injected right now. Think about how we can inject static prices.
            if (this.HasMilk)
            {
                cost += CondimentPrices.MilkCost;
            }

            if (this.HasSugar)
            {
                cost += CondimentPrices.SugarCost;
            }

            return cost;
        }

        public bool HasMilk { get; set; }
        public bool HasSugar { get; set; }
    }
}
