using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcuCafe.Condiments;

namespace AcuCafe.Drinks
{
    public class HotTea : Drink
    {
        public override List<Type> ValidCondiments { get { return new List<Type>() { typeof(Sugar), typeof(Milk) }; } }

        public override string Description
        {
            get { return "Hot tea"; }
        }

        protected override double BaseCost { get { return 1; } }
    }
}
