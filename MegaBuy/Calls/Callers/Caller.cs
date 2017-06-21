using System;
using MegaBuy.Calls.Events;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using System.Collections.Generic;
using MegaBuy.Calls.Messages;

namespace MegaBuy.Calls.Callers
{
    public sealed class Caller : IAutomaton, IDisposable
    {
        public CallerPatience Patience = CallerStartingPatience.New;
        public string FirstName => Name.Split(' ')[0];
        public string Name { get; }
        public Dictionary<string, string> Traits { get; }

        private readonly int _patienceLossRateMs;
        private readonly Chat _chat;
        private int _gracePeriods;
        private double _elapsedMs;

        public Caller(Chat chat, int patienceLossRateMs, Dictionary<string, string> traits)
            : this(chat, CallerNames.Random, patienceLossRateMs, traits) { }

        public Caller(Chat chat, string name, int patienceLossRateMs, Dictionary<string, string> traits)
        {
            _chat = chat;
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
                World.Publish(new CallResolved(CallResolution.CallerHangUp));
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
            if (Patience.Value < 10 && Patience.Value % 3 == 0)
                Complain();
        }

        private void Complain()
        {
            _chat.CallerSays("Are you still there?");
        }

        private void SocialMistakeOccurred(SocialMistakeOccurred mistake)
        {
            Patience.ReduceBy(mistake.PatiencePenalty);
        }

        public bool TraitMatches(string trait, string value)
        {
            return Traits.ContainsKey(trait) && Traits[trait].Equals(value);
        }

        public bool IsAtMost(string trait, int value)
        {
            return Traits.ContainsKey(trait) && int.Parse(Traits[trait]) <= value;
        }

        public bool IsAtLeast(string trait, int value)
        {
            return Traits.ContainsKey(trait) && int.Parse(Traits[trait]) >= value;
        }

        public bool HasTrait(string trait)
        {
            return Traits.ContainsKey(trait) && Traits[trait] == "true";
        }

        public void Dispose()
        {
            World.Unsubscribe(this);
        }
    }
}
