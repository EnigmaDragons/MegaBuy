using MegaBuy.Calls.Rules;
using System;
using MegaBuy.Jobs;
using MegaBuy.Jobs.Referrer;
using MegaBuy.MegaBuyCorporation.JobRoles.Referrer;

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
            return RoleTraits.Calls[_position]();
        }
    }
}
