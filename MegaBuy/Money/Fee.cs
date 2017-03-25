using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaBuy.Money
{
    public class Fee : SimpleAmount
    {
        public Fee(decimal amount) : base(amount) { }
    }
}
