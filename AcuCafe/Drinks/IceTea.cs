using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcuCafe.Drinks
{
    public class IceTea : Drink
    {
        public override string Description
        {
            get { return "Ice tea"; }
        }

        protected override double innerCost { get { return 1.5; } }
    }
}
