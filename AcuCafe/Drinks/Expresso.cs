using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcuCafe.Condiments;

namespace AcuCafe.Drinks
{
    public class Expresso : Drink
    {
        public override List<Type> ValidCondiments { get { return new List<Type>() { typeof(Sugar), typeof(Milk), typeof(Chocolate) }; } }

        public override string Description
        {
            get { return "Expresso"; }
        }

        protected override double BaseCost { get { return 1.8; } }
    }
}
