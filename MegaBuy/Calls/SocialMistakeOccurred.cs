
namespace MegaBuy.Calls
{
    public struct SocialMistakeOccurred
    {
        public int PatiencePenalty { get; }

        public SocialMistakeOccurred(int patiencePenalty)
        {
            PatiencePenalty = patiencePenalty;
        }
    }
}
