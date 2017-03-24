using MonoDragons.Core.Engine;

namespace MegaBuy.Calls.Rules
{
    public static class CallerPatienceDecay
    {
        private static readonly Map<int, int> _dayDecayMs = new Map<int, int>
        {
            {1, 5000},
        };

        public static int CallDecayForDay(int day)
        {
            return _dayDecayMs[day];
        }
    }
}
