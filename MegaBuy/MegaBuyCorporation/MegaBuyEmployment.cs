using MegaBuy.Calls.Events;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MegaBuy.Notifications;
using MegaBuy.Calls;
using MegaBuy.MegaBuyCorporation.Policies;
using MegaBuy.Jobs;

namespace MegaBuy.MegaBuyCorporation
{
    public class MegaBuyEmployment
    {
        private readonly ActivePolicies _policies;

        private int _numMistakesInCurrentDay;
        private int _numResolvedCallsInCurrentDay;
        private Job role;
        
        public MegaBuyEmployment(ActivePolicies policies)
        {
            _policies = policies;
            role = Job.ReturnSpecialistLevel1;
            World.Subscribe(EventSubscription.Create<HourChanged>(HourChanged, this));
            World.Subscribe(EventSubscription.Create<CallResolved>(CallResolved, this));
            World.Subscribe(EventSubscription.Create<TechnicalMistakeOccurred>(TechnicalMistakeOccurred, this));
            World.Subscribe(EventSubscription.Create<JobAccepted>(x => AcceptPromotion(x.Job), this));
            World.Subscribe(EventSubscription.Create<JobDeclined>(x => DeclinePromotion(), this));
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
            if(role == Job.ReferrerLevel1)
            {
                World.Publish(new PlayerNotification("MegaBuy",
                    "You have been performing excellently. Since you have been doing so good you will be offered a promotion!"));
                World.Publish(new JobRoleOffered("", Job.ReferrerLevel2));
            }
        }

        private void AcceptPromotion(Job role)
        {
            // @todo #1 fix current game state so it exists before this object is initialized
            _policies.Clear();
            _policies.Add(JobTraits.Policies[role]);
            World.Publish(new PolicyChanged());
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
