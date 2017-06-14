using System;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Conversation_Pieces;
using MegaBuy.Calls.Events;
using MegaBuy.Calls.Messages;
using MegaBuy.Calls.Ratings;
using MegaBuy.Calls.Rules;
using MegaBuy.MegaBuyCorporation.Policies;
using MegaBuy.Money.Amounts;
using MegaBuy.PurchaseHistories;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Calls
{
    public sealed class Call : IAutomaton, IDisposable
    {
        private static readonly Policy DefaultPolicy = new Policy("All valid customer requests must be approved.", x => true, CallResolution.Reject);
        private readonly CallResolution _correctResolution;
        private readonly ActivePolicies _activePolicies;

        public List<ICallOption> Options { get; }
        public Script Script { get; }
        public CallScenario Scenario { get; }

        public Caller Caller => Scenario.Caller;
        public IEnumerable<Purchase> Purchases => Scenario.Purchases;
        public Optional<Purchase> Purchase => Scenario.Target;

        public Call(Script script, CallScenario scenario, CallResolution correctResolution, List<ICallOption> options)
        {
            _activePolicies = CurrentGameState.State.ActivePolicies;
            Script = script;
            Scenario = scenario;
            _correctResolution = correctResolution;
            Options = options;
            World.Subscribe(EventSubscription.Create<CallResolved>(ResolveCall, this));
        }

        public void Update(TimeSpan delta)
        {
            Caller.Update(delta);
        }

        public void Dispose()
        {
            Caller.Dispose();
            World.Unsubscribe(this);
        }

        private void ResolveCall(CallResolved callResolved)
        {
            var res = callResolved.Resolution;
            var violations = _activePolicies.GetViolations(callResolved.Resolution, this);
            violations.ForEach(x => World.Publish(new TechnicalMistakeOccurred(new PayPenalty(5), x)));
            if (res != _correctResolution && res == CallResolution.Reject && !violations.Any())
                World.Publish(new TechnicalMistakeOccurred(new PayPenalty(5), DefaultPolicy));

            if (res == _correctResolution)
            {
                World.Publish(new CallSucceeded(GetHashCode()));
                World.Publish(new CallRated(GetHashCode(), CallerPatienceCallRatings.Get(Caller.Patience)));
            }
            else
            {
                World.Publish(new CallFailed(new Fee(2)));
                World.Publish(new CallRated(GetHashCode(), new CallRating(1)));
            }
            Dispose();
        }
    }
}
