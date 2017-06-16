using System;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Calls;
using MegaBuy.Calls.Callers;
using MegaBuy.Calls.Events;
using MegaBuy.Reports;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.MegaBuyCorporation
{
    public sealed class MegaBuyReporting
    {
        private readonly int _expectedCalls = 12;
        private readonly double _expectedHoursWorked = 10;
        private readonly double _expectedAvgRating = 2.5f;

        private readonly List<CallerFeedback> _feedback = new List<CallerFeedback>();
        private double _currentHoursWorked = 0;
        private int _currentCalls = 0;
        private bool _isWorking;

        public MegaBuyReporting()
        {
            World.Subscribe(EventSubscription.Create<CallRated>(FeedbackReceived, this));
            World.Subscribe(EventSubscription.Create<DayEnded>(SendReport, this));
            World.Subscribe(EventSubscription.Create<CallStarted>(x => _currentCalls++, this));
            World.Subscribe(EventSubscription.Create<AgentCallStatusChanged>(UpdateWorkStatus, this));
            World.Subscribe(EventSubscription.Create<MinuteChanged>(ClockWork, this));
        }

        private void ClockWork(MinuteChanged obj)
        {
            if (_isWorking)
                _currentHoursWorked += (double)1 / 60;
        }

        private void UpdateWorkStatus(AgentCallStatusChanged status)
        {
            _isWorking = status.Status == AgentCallStatus.Available || status.Status == AgentCallStatus.InCall;
        }

        private void SendReport(DayEnded obj)
        {
            World.Publish(new WorkReportPublished(MakeReport()));
            _feedback.Clear();
            _currentHoursWorked = 0;
            _currentCalls = 0;
        }

        private void FeedbackReceived(CallRated call)
        {
            _feedback.Add(call.Feedback);
        }

        private WorkReportData MakeReport()
        {
            var bestFeedback = _feedback.Count > 0 ? _feedback.OrderBy(x => x.RatingScore).Last() : CallerFeedback.None;
            var worstFeedback = _feedback.Count > 0 ? _feedback.OrderBy(x => x.RatingScore).First() : CallerFeedback.None;
            var avgCallRating = _feedback.Count > 0 ? _feedback.Average(x => x.RatingScore) : 0;

            return new WorkReportData
            {
                AvgCallRatingActual = avgCallRating,
                AvgCallRatingExpected = _expectedAvgRating,
                CallsAnsweredActual = _currentCalls,
                CallsAnsweredExpected = _expectedCalls,
                HoursWorkedActual = Math.Round(_currentHoursWorked, 2),
                HoursWorkedExpected = Math.Round(_expectedHoursWorked, 2),
                BestFeedback = bestFeedback,
                WorstFeedback = worstFeedback
            };
        }
    }
}
