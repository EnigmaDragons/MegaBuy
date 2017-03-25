using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaBuy.Calls
{
    public interface ICallOption
    {
        string message { get; }
        void Go(bool IsCorrect);
    }
}
