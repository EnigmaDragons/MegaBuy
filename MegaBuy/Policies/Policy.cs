
namespace MegaBuy.Policies
{
    public sealed class Policy
    {
        private readonly string _text;

        public Policy(string text)
        {
            _text = text;
        }

        public string Text()
        {
            return _text;
        }
    }
}
