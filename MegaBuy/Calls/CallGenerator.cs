using MegaBuy.Jobs;

namespace MegaBuy.Calls
{
    public class CallGenerator
    {
        private Job _position;

        public CallGenerator(Job position)
        {
            _position = position;
        }

        public void PositionChanged(Job postion)
        {
            _position = postion;
        }

        public Call GenerateCall()
        {
            return JobTraits.Calls[_position]();
        }
    }
}
