using MegaBuy.Calls.PositionBasedCall;
using MegaBuy.Calls.Rules;
using System;

namespace MegaBuy.Calls
{
    public class CallGenerator
    {
        private JobRole _position;

        public CallGenerator(JobRole position)
        {
            _position = position;
        }

        public void PositionChanged(JobRole postion)
        {
            _position = postion;
        }

        public Call GenerateCall()
        {
            if (_position == JobRole.ReferrerLevel1)
                return ReferrerLevel1.NewCall();
            throw new Exception();
        }
    }
}
