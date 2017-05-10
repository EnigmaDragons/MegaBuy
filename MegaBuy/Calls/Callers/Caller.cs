using System;
using MegaBuy.Calls.Conversation_Pieces;
using MegaBuy.Calls.Events;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using System.Collections.Generic;

namespace MegaBuy.Calls.Callers
{
    public sealed class Caller : IAutomaton
    {
        public CallerPatience Patience = CallerStartingPatience.New;
        public string Name { get; }
        public Dictionary<string, string> Traits { get; }

        private readonly int _patienceLossRateMs;
        private int _gracePeriods;

        private double _elapsedMs;

        public Caller(int patienceLossRateMs, Dictionary<string, string> traits)
            : this(CallerNames.Random, patienceLossRateMs, traits) { }

        public Caller(string name, int patienceLossRateMs, Dictionary<string, string> traits)
        {
            Traits = traits;
            Name = name;
            _patienceLossRateMs = patienceLossRateMs;
            _gracePeriods = 3;
            World.Subscribe(EventSubscription.Create<SocialMistakeOccurred>(SocialMistakeOccurred, this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => World.Unsubscribe(this), this));
        }

        public void Update(TimeSpan delta)
        {
            _elapsedMs += delta.TotalMilliseconds;
            UpdatePatience();
            if (Patience.Value == 0)
                World.Publish(new CallFailed());
        }

        private void UpdatePatience()
        {
            if (_elapsedMs < _patienceLossRateMs) return;

            _elapsedMs -= _patienceLossRateMs;
            if (_gracePeriods > 0)
            {
                _gracePeriods--;
                return;
            }

            Patience.ReduceBy(1);
        }

        private void SocialMistakeOccurred(SocialMistakeOccurred mistake)
        {
            Patience.ReduceBy(mistake.PatiencePenalty);
        }

        public bool TraitMatches(string trait, string value)
        {
            return Traits.ContainsKey(trait) && Traits[trait].Equals(value);
        }
    }
}
