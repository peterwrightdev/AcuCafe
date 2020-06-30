using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcuCafe.Drinks
{
    public class Tea : Drink
    {
        public override string Description
        {
            get { return "Hot tea"; }
        }

        protected override double innerCost { get { return 1; } }
    }
}
