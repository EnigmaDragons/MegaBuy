﻿using System;
using MegaBuy.Calls.Events;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MegaBuy.Calls.Rules;
using MegaBuy.Notifications;
using MegaBuy.Money;
using MegaBuy.MegaBuyCorporation.JobRoles.Referrer;
using MegaBuy.Calls;
using MegaBuy.MegaBuyCorporation.Policies;
using MegaBuy.Policies;
using MegaBuy.Jobs;
using MegaBuy.Jobs.Referrer;

namespace MegaBuy.MegaBuyCorporation
{
    public class MegaBuyEmployment
    {
        private readonly ActivePolicies _policies;

        private int _numMistakesInCurrentDay;
        private int _numResolvedCallsInCurrentDay;
        private JobRole role;
        
        public MegaBuyEmployment(ActivePolicies policies)
        {
            _policies = policies;
            role = JobRole.ReferrerLevel1;
            World.Subscribe(EventSubscription.Create<HourChanged>(HourChanged, this));
            World.Subscribe(EventSubscription.Create<CallResolved>(CallResolved, this));
            World.Subscribe(EventSubscription.Create<TechnicalMistakeOccurred>(TechnicalMistakeOccurred, this));
            World.Subscribe(EventSubscription.Create<JobRoleAccepted>(x => AcceptPromotion(x.JobRole), this));
            World.Subscribe(EventSubscription.Create<JobRoleDeclined>(x => DeclinePromotion(), this));
        }

        private void CallResolved(CallResolved obj)
        {
            _numResolvedCallsInCurrentDay++;
        }

        private void HourChanged(HourChanged hourChanged)
        {
            if (hourChanged.Hour == 20)
                WorkDayEnded();
        }

        private void WorkDayEnded()
        {
            if (_numResolvedCallsInCurrentDay > 15)
                OfferPromotion();
            _numMistakesInCurrentDay = 0;
            _numResolvedCallsInCurrentDay = 0;
        }

        private void OfferPromotion()
        {
            if(role == JobRole.ReferrerLevel1)
            {
                World.Publish(new PlayerNotification("MegaBuy",
                    "You have been performing excellently. Since you have been doing so good you will be offered a promotion!"));
                World.Publish(new JobRoleOffered("", JobRole.ReferrerLevel2));
            }
        }

        private void AcceptPromotion(JobRole role)
        {
            // @ todo #1 fix current game state so it exists before this object is initialized
            var accounting = (MegaBuyAccounting)CurrentGameState.GameState.SingleInstanceSubscriptions[typeof(MegaBuyAccounting)];
            accounting.ChangePaymentPlans(RoleTraits.Rates[role]);
            _policies.Clear();
            _policies.Add(RoleTraits.Policies[role]);
            World.Publish(new PolicyChanged());
            var queue = (CallQueue)CurrentGameState.GameState.SingleInstanceSubscriptions[typeof(CallQueue)];
            queue.ChangePlayerRole(role);
        }

        private void DeclinePromotion()
        {

        }

        private void TechnicalMistakeOccurred(TechnicalMistakeOccurred obj)
        {
            _numMistakesInCurrentDay++;
            if (_numMistakesInCurrentDay == 7)
                World.NavigateToScene("Fired");
        }
    }
}
