using System;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Events;
using MegaBuy.Calls.Messages;
using MegaBuy.Calls.Ratings;
using MegaBuy.Calls.Rules;
using MegaBuy.MegaBuyCorporation.Policies;
using MegaBuy.Money.Amounts;
using MegaBuy.Policies;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Calls
{
    public sealed class Call : IAutomaton
    {
        private readonly CallResolution _correctResolution;
        private readonly ActivePolicies _activePolicies;

        public List<ICallOption> Options { get; }
        public Caller Caller { get; }
        public Script Script { get; }

        public Call(Caller caller, Script script, CallResolution correctResolution, List<ICallOption> options)
        {
            _activePolicies = GameState.ActivePolicies;
            Caller = caller;
            Script = script;
            _correctResolution = correctResolution;
            Options = options;
            World.Subscribe(EventSubscription.Create<CallResolved>(ResolveCall, this));
        }

        public void Update(TimeSpan delta)
        {
            Caller.Update(delta);
        }

        private void ResolveCall(CallResolved callResolved)
        {
            var violations = _activePolicies.GetViolations(callResolved.Resolution, this);
            violations.ForEach(x => World.Publish(new TechnicalMistakeOccurred(new PayPenalty(5), x)));

            if (callResolved.Resolution == _correctResolution)
            {
                World.Publish(new CallSucceeded(GetHashCode()));
                World.Publish(new CallRated(GetHashCode(), CallerPatienceCallRatings.Get(Caller.Patience)));
            }
            else
            {
                World.Publish(new CallFailed(new Fee(2)));
                World.Publish(new CallRated(GetHashCode(), new CallRating(1)));
            }
            World.Unsubscribe(this);
        }
    }
}
