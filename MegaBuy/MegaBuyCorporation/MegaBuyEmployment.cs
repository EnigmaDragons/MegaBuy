using System;
using MegaBuy.Calls.Events;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MegaBuy.Calls.Rules;
using MegaBuy.Notifications;
using MegaBuy.Money;
using MegaBuy.MegaBuyCorporation.JobRoles.Referrer;
using MegaBuy.Calls;

namespace MegaBuy.MegaBuyCorporation
{
    public class MegaBuyEmployment
    {
        private int _numMistakesInCurrentDay;
        private int _numResolvedCallsInCurrentDay;
        private JobRole role;

        public MegaBuyEmployment()
        {
            role = JobRole.ReferrerLevel1;
            World.Subscribe(EventSubscription.Create<HourChanged>(HourChanged, this));
            World.Subscribe(EventSubscription.Create<CallResolved>(CallResolved, this));
            World.Subscribe(EventSubscription.Create<TechnicalMistakeOccurred>(TechnicalMistakeOccurred, this));
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
                World.Publish(new PromotionOption("", JobRole.ReferrerLevel2,
                    () => AcceptPromotion(JobRole.ReferrerLevel2), () => DeclinePromotion()));
            }
        }

        private void AcceptPromotion(JobRole role)
        {
            if (role == JobRole.ReferrerLevel2)
            {
                var accounting = (MegaBuyAccounting)GameState.SingleInstanceSubscriptions[typeof(MegaBuyAccounting)];
                accounting.ChangePaymentPlans(ReferrerPerCallRates.Level2PerCallRate);
                GameState.ActivePolicies = new Policies.ActivePolicies();
                GameState.ActivePolicies.Add(ReferrerPolicies.Level2Policies);
                var queue = (CallQueue)GameState.SingleInstanceSubscriptions[typeof(CallQueue)];
                queue.ChangePlayerRole(role);
            }
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
