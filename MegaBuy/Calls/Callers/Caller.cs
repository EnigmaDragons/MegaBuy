using System;
using MegaBuy.Calls.Events;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using System.Collections.Generic;
using System.Diagnostics;
using MegaBuy.Calls.Conversation_Pieces;
using MegaBuy.Calls.Messages;
using MonoDragons.Core.Common;

namespace MegaBuy.Calls.Callers
{
    public sealed class Caller : IAutomaton, IDisposable
    {
        private const int MaxGracePeriods = 3;
        private const double ComplaintImpatienceFactor = 1.2;

        public CallerPatience Patience = CallerStartingPatience.New;
        public string FirstName => Name.Split(' ')[0];
        public string Name { get; }
        public Dictionary<string, string> Traits { get; }

        private readonly int _originalPatienceLossRateMs;
        private readonly Chat _chat;

        private bool AnyNewChatMessages => _lastChatMessage < _chat.Count;

        private double _patienceLossRateMs;
        private int _gracePeriods;
        private double _elapsedMs;
        private int _lastChatMessage;

        public Caller(Chat chat, int patienceLossRateMs, Dictionary<string, string> traits)
            : this(chat, CallerNames.Random, patienceLossRateMs, traits) { }

        public Caller(Chat chat, string name, int patienceLossRateMs, Dictionary<string, string> traits)
        {
            _chat = chat;
            _lastChatMessage = _chat.Count;
            Traits = traits;
            Name = name;
            _originalPatienceLossRateMs = patienceLossRateMs;
            ResetPatience();
            World.Subscribe(EventSubscription.Create<SocialMistakeOccurred>(SocialMistakeOccurred, this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => World.Unsubscribe(this), this));
            World.Subscribe(EventSubscription.Create<CallerHangupFinished>(x => OnCallerHangupFinished(), this));
        }

        public void Update(TimeSpan delta)
        {
            _elapsedMs += delta.TotalMilliseconds;
            ProcessNewChatMessages();
            UpdatePatience();
        }

        private void OnCallerHangupFinished()
        {
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
            ComplainIfImpatient();
            HangupIfOutOfPatience();
        }

        private void ComplainIfImpatient()
        { 
            if (Patience.Value < 10 && Patience.Value > 0 && Patience.Value % 3 == 0)
                Complain();

            if (Patience.Value == 0 && Rng.Dbl() < 0.5)
                AnnouceHangingUp();
        }

        private void HangupIfOutOfPatience()
        {
            if (Patience.Value == 0)
            {
                _chat.CallerSays(new RandomHangup());
                World.Publish(new CallerHangupStarted());
            }
        }

        private void AnnouceHangingUp()
        {
            _chat.CallerSays("You're so slow! I gotta go.");
        }

        private void Complain()
        {
            _chat.CallerSays(new RandomComplaint());
            _lastChatMessage = _chat.Count;
            _patienceLossRateMs *= 1 / ComplaintImpatienceFactor;
        }

        private void ProcessNewChatMessages()
        {
            if (!AnyNewChatMessages)
                return;

            _lastChatMessage = _chat.Count;
            ResetPatience();
        }

        private void ResetPatience()
        {
            _patienceLossRateMs = _originalPatienceLossRateMs;
            _gracePeriods = MaxGracePeriods;
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
