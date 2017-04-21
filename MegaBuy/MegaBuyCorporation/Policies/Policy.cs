using System;
using MegaBuy.Calls;
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Rules;

namespace MegaBuy.Policies
{
    public sealed class Policy
    {
        private readonly CallResolution _resolution;
        private readonly Predicate<Caller> _condition;

        public string Text { get; }

        public Policy(string text, CallResolution resolution, Predicate<Caller> condition)
        {
            _resolution = resolution;
            _condition = condition;
            Text = text;
        }

        public bool MeetsPolicy(CallResolution resolution, Call call)
        {
            return !Applies(resolution) || _condition(call.Caller);
        }

        private bool Applies(CallResolution resolution)
        {
            return resolution.Equals(_resolution) || resolution.Equals(CallResolution.Any);
        }
    }
}
