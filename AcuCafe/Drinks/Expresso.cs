using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcuCafe.Drinks
{
    public class Expresso : Drink
    {
        public override string Description
        {
            get { return "Expresso"; }
        }

        protected override double innerCost { get { return 1.8; } }
    }
}
