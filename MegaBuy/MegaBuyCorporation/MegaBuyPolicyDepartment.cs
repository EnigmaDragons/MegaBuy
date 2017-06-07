﻿using MegaBuy.Jobs.ReturnSpecialist;
using MegaBuy.MegaBuyCorporation.Policies;
using MegaBuy.Notifications;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using System.Collections.Generic;
using System.Linq;

namespace MegaBuy.MegaBuyCorporation
{
    public sealed class MegaBuyPolicyDepartment
    {
        private readonly ActivePolicies _policies;
        private readonly List<Policy> _futurePolicies;

        public MegaBuyPolicyDepartment(ActivePolicies policies)
        {
            _futurePolicies = ReturnSpecialistPolicies.Level2.ToList();
            _policies = policies;
            World.SubscribeForever(EventSubscription.Create<DayStarted>(DayStarted, this));
        }

        private void DayStarted(DayStarted day)
        {
            _policies.Add(GetNewPolicy());
            World.Publish(new PlayerNotification("MegaBuy Policy Dept.", $"{CurrentGameState.State.Clock.Date}: We have updated our company policies."));
        }

        private Policy GetNewPolicy()
        {
            var policy = _futurePolicies.First();
            _futurePolicies.Remove(policy);
            return policy;
        }
    }
}
