using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaBuy.Calls.Options
{
    public class TranslateButton : ICallOption
    {
        public string message
        {
            get
            {
                return "Use the translate button.";
            }
        }

        public void Go(bool IsCorrect)
        {
        }
    }
}
