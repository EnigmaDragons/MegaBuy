
namespace MegaBuy.Calls.Rules
{
    public static class CallerStartingPatience
    {
        public static CallerPatience New =>  new CallerPatience(15);
    }
}
