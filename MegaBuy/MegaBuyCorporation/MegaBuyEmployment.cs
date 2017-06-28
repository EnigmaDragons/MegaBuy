using MegaBuy.Calls.Events;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MegaBuy.Notifications;
using MegaBuy.MegaBuyCorporation.Policies;
using MegaBuy.Jobs;

namespace MegaBuy.MegaBuyCorporation
{
    public class MegaBuyEmployment
    {
        private const int MaxMistakes = 7;
        private const int PenultimateMistake = 6;
        private const int AntepenultimateMistake = 5;

        private readonly ActivePolicies _policies;

        private int _numMistakesInCurrentDay;
        private int _numResolvedCallsInCurrentDay;
        private Job _job;
        
        public MegaBuyEmployment(ActivePolicies policies)
        {
            _policies = policies;
            _job = Job.ReturnSpecialistLevel1;
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
        }

        private void AcceptPromotion(Job role)
        {
            _policies.Clear();
            _policies.Add(JobTraits.Policies[role]);
            World.Publish(new PlayerNotification("MegaBuy", "Congrats on gaining " + role.ToString() + "."));
            World.Publish(new PoliciesChanged());
            World.Publish(new JobChanged(role));
        }

        private void DeclinePromotion()
        {

        }

        private void TechnicalMistakeOccurred(TechnicalMistakeOccurred obj)
        {
            _numMistakesInCurrentDay++;
            if (_numMistakesInCurrentDay == PenultimateMistake)
                World.Publish(new PlayerNotification("MegaBuy", "FINAL WARNING: Your work does not meet minimum quality standards."));
            if (_numMistakesInCurrentDay == AntepenultimateMistake)
                World.Publish(new PlayerNotification("MegaBuy", "WARNING: Your work quality is below our expectations."));
            if (_numMistakesInCurrentDay == MaxMistakes)
                World.NavigateToScene("Fired");
        }
    }
}
