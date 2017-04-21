using MegaBuy.Calls.Rules;
using System;
using MegaBuy.JobRoles.Referrer;

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
                return ReferrerCalls.NewLevel1Call();
            if (_position == JobRole.ReferrerLevel2)
                return ReferrerCalls.NewLevel2Call();
            throw new Exception("Unknown Job Role");
        }
    }
}
