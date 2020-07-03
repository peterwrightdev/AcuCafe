using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcuCafe.Condiments;

namespace AcuCafe.Drinks
{
    public class IceTea : Drink
    {
        public override List<Type> ValidCondiments { get { return new List<Type>() { typeof(Sugar) }; } }

        public override string Description
        {
            get { return "Ice tea"; }
        }

        protected override double BaseCost { get { return 1.5; } }
    }
}
