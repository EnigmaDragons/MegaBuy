﻿using System;
using MegaBuy.Calls.Conversation_Pieces;
using MegaBuy.Calls.Events;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Calls.Callers
{
    public sealed class Caller : IAutomaton
    {
        public CallerPatience Patience = CallerStartingPatience.New;
        public string Name { get; }

        private readonly int _patienceLossRateMs;
        private int _gracePeriods;

        private double _elapsedMs;

        public Caller()
            : this(CallerNames.Random, PatienceLevel.Random) { }

        public Caller(string name)
            : this(name, PatienceLevel.Random) { }

        public Caller(string name, int patienceLossRateMs)
        {
            Name = name;
            _patienceLossRateMs = patienceLossRateMs;
            _gracePeriods = 3;
            World.SubscribeForScene(EventSubscription.Create<SocialMistakeOccurred>(SocialMistakeOccurred, this));
            World.SubscribeForScene(EventSubscription.Create<CallResolved>(x => World.Unsubscribe(this), this));
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
    }
}