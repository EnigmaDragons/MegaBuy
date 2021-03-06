﻿using System;
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
        private readonly ActivePolicies _activePolicies;
        private readonly CallResolution _correctResolution;

        public List<ICallOption> Options { get; }
        public Chat Chat { get; }
        public CallScenario Scenario { get; }
        
        public Caller Caller => Scenario.Caller;
        public IEnumerable<Purchase> Purchases => Scenario.Purchases;
        public Optional<Purchase> Purchase => Scenario.Target;

        public Call(Chat chat, CallScenario scenario, CallResolution correctResolution, List<ICallOption> options)
        {
            _activePolicies = CurrentGameState.State.ActivePolicies;
            Chat = chat;
            Scenario = scenario;
            _correctResolution = correctResolution;
            Options = options;
            CurrentScene.Add(this);
            World.Subscribe(EventSubscription.Create<CallResolved>(ResolveCall, this));
        }

        public void Update(TimeSpan delta)
        {
            Caller.Update(delta);
        }

        public void Dispose()
        {
            World.Unsubscribe(this);
            CurrentScene.Remove(this);
        }

        private void ResolveCall(CallResolved callResolved)
        {
            var res = callResolved.Resolution;
            var violations = _activePolicies.GetViolations(callResolved.Resolution, new ResolvedCall(Purchase, _correctResolution, res));

            if (violations.Any())
                World.Publish(new TechnicalMistakeOccurred(new PayPenalty(5), violations.First()));
            if (res != _correctResolution && res == CallResolution.Reject && !violations.Any())
                World.Publish(new TechnicalMistakeOccurred(new PayPenalty(5), DefaultPolicy));

            if (res == _correctResolution)
            {
                var rating = CallerPatienceCallRatings.Get(Caller.Patience);
                World.Publish(new CallSucceeded(GetHashCode()));
                World.Publish(new CallRated(GetHashCode(), new CallerFeedback(rating, CallerReviews.Get(rating))));
            }
            else
            {
                World.Publish(new CallFailed(new Fee(2)));
                World.Publish(new CallRated(GetHashCode(), new CallerFeedback(new CallRating(1), CallerReviews.Get(1))));
            }
            Dispose();
        }
    }
}
