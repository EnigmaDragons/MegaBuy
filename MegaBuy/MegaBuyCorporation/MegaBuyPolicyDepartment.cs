using MegaBuy.Jobs.ReturnSpecialist;
using MegaBuy.MegaBuyCorporation.Policies;
using MegaBuy.Notifications;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Reports;

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
            World.Subscribe(EventSubscription.Create<WorkReportPublished>(WorkReportReceived, this));
        }

        private void WorkReportReceived(WorkReportPublished obj)
        {
            _policies.Add(NextPolicy());
            if (obj.Report.PerformanceRating == MegaBuyPerformanceRating.Good)
                _policies.Add(NextPolicy());
            World.Publish(new PlayerNotification("MegaBuy Policy Dept.", $"{CurrentGameState.State.Clock.Date}: We have updated our company policies."));
        }

        private Policy NextPolicy()
        {
            var policy = _futurePolicies.First();
            _futurePolicies.Remove(policy);
            return policy;
        }
    }
}
