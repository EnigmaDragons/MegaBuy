using System;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Calls;
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Rules;

namespace MegaBuy.MegaBuyCorporation.Policies
{
    public sealed class Policy
    {
        private readonly List<CallResolution> _resolutions;
        private readonly Predicate<Caller> _condition;

        public string Text { get; }

        public Policy(string text, CallResolution resolution, Predicate<Caller> condition)
            : this(text, condition, resolution) { }

        public Policy(string text, Predicate<Caller> condition, params CallResolution[] resolutions)
        {
            _resolutions = resolutions.ToList();
            _condition = condition;
            Text = text;
        }

        public bool MeetsPolicy(CallResolution resolution, Call call)
        {
            return !Applies(resolution) || _condition(call.Caller);
        }

        private bool Applies(CallResolution resolution)
        {
            return resolution.Equals(CallResolution.Any) || _resolutions.Contains(resolution);
        }
    }
}
