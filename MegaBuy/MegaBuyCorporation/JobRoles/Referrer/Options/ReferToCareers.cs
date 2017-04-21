﻿using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;

namespace MegaBuy.JobRoles.Referrer.Options
{
    public class ReferToCareers : ICallOption
    {
        public string Description => "Careers";

        public void Go()
        {
            World.Publish(new CallResolved(CallResolution.ReferToCareers));
        }
    }
}
