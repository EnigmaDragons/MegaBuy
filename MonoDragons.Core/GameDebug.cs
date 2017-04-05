using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaBuy
{
    public static class GameDebug
    {
        public static int x = 0;
        public static int frames = 0;
        public static double averageXPerFrame { get { return x / frames; } }
    }
}
