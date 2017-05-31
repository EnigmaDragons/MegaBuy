using System.Collections.Generic;
using MonoDragons.Core.Common;

namespace MegaBuy.Calls.Rules
{
    public static class PatienceLevel
    {
        public static int VeryPatient = 6000;
        public static int Patient = 5000;
        public static int Average = 4000;
        public static int Impatient = 3000;
        public static int VeryImpatient = 2000;

        private static readonly List<int> RandomOptions = new List<int> {VeryImpatient, Patient, Average, Impatient, VeryImpatient};

        public static int Random => RandomOptions.Random();
    }
}
