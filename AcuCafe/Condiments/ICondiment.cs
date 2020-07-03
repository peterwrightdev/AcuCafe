using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcuCafe.Condiments
{
    public interface ICondiment
    {
        // Price of condiments has been moved to here rather than a const property on the Drink base class.
        // This is because the cost of a condiment is a property of the condiment, not the drink.
        double Cost { get; }
    }
}
