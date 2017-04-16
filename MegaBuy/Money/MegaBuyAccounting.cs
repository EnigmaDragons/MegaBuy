using System.Collections.Generic;
using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MegaBuy.Money.Accounts;
using MegaBuy.Money.Amounts;
using MegaBuy.Money.Rules;
using MegaBuy.Notifications;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Money
{
    public sealed class MegaBuyAccounting
    {
        private readonly IAccount _playerAccount;

        private IPerCallRate _currentRate;
        private DayPayment _dayPayment;

        private int _numMistakesInCurrentDay;

        private readonly List<int> _pendingPayments;

        public MegaBuyAccounting(IAccount playerAccount)
        {
            _playerAccount = playerAccount;
            _currentRate = new Day1PerCallRate();
            _pendingPayments = new List<int>();
            _dayPayment = new DayPayment();
            World.Subscribe(EventSubscription.Create<HourChanged>(HourChanged, this));
            World.Subscribe(EventSubscription.Create<CallSucceeded>(CallSucceeded, this));
            World.Subscribe(EventSubscription.Create<CallRated>(CallRated, this));
            World.Subscribe(EventSubscription.Create<TechnicalMistakeOccurred>(TechnicalMistakeOccurred, this));
        }

        private void CallSucceeded(CallSucceeded call)
        {
            _pendingPayments.Add(call.CallId);
        }

        private void CallRated(CallRated rated)
        {
            _pendingPayments.Remove(rated.CallId);
            _dayPayment.Add(new CallPayment(_currentRate, rated.Rating));
        }

        private void HourChanged(HourChanged hourChanged)
        {
            if (hourChanged.Hour == 20)
                WorkDayEnded();
        }

        private void WorkDayEnded()
        {
            _playerAccount.Add(_dayPayment);
            World.Publish(new PlayerNotification("MegaBuy", $"You have been paid MBit - {_dayPayment.Amount()}"));
            _dayPayment = new DayPayment();
            _numMistakesInCurrentDay = 0;
        }

        private void TechnicalMistakeOccurred(TechnicalMistakeOccurred mistake)
        {
            _dayPayment.Remove(mistake.PayPenalty);
            World.Publish(new PlayerNotification("MegaBuy", mistake.Policy.Text));
            _numMistakesInCurrentDay++;
            if (_numMistakesInCurrentDay == 7)
                World.NavigateToScene("Fired");
        }
    }
}
