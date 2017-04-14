using System.Collections.Generic;
using MonoDragons.Core.Common;

namespace MegaBuy.Calls.Rules
{
    public static class PatienceLevel
    {
        public static int VeryPatient = 3000;
        public static int Patient = 2500;
        public static int Average = 2000;
        public static int Impatient = 1500;
        public static int VeryImpatient = 1000;

        private static readonly List<int> RandomOptions = new List<int> {VeryImpatient, Patient, Average, Impatient, VeryImpatient};

        public static int Random => RandomOptions.Random();
    }
}
