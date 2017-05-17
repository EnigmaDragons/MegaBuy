using System.Collections.Generic;
using MegaBuy.Calls.Events;
using MegaBuy.Money.Accounts;
using MegaBuy.Money.Amounts;
using MegaBuy.Notifications;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MegaBuy.MegaBuyCorporation.JobRoles.Referrer;
using System;

namespace MegaBuy.Money
{
    public sealed class MegaBuyAccounting
    {
        private readonly IAccount _playerAccount;

        private PerCallRate _currentRate;
        private DayPayment _dayPayment;

        private readonly List<int> _pendingPayments;

        public MegaBuyAccounting(IAccount playerAccount, PerCallRate payment)
        {
            _playerAccount = playerAccount;
            _currentRate = payment;
            _pendingPayments = new List<int>();
            _dayPayment = new DayPayment();
            World.Subscribe(EventSubscription.Create<HourChanged>(HourChanged, this));
            World.Subscribe(EventSubscription.Create<CallSucceeded>(CallSucceeded, this));
            World.Subscribe(EventSubscription.Create<CallRated>(CallRated, this));
            World.Subscribe(EventSubscription.Create<TechnicalMistakeOccurred>(TechnicalMistakeOccurred, this));
            World.Subscribe(EventSubscription.Create<CallFailed>(CallFailed, this));
        }

        private void CallFailed(CallFailed call)
        {
            _dayPayment.Remove(call.PayPenalty);
        }

        public void ChangePaymentPlans(PerCallRate payment)
        {
            _currentRate = payment;
        }

        private void CallSucceeded(CallSucceeded call)
        {
            _pendingPayments.Add(call.CallId);
        }

        private void CallRated(CallRated rated)
        {
            if (_pendingPayments.Contains(rated.CallId))
            {
                _pendingPayments.Remove(rated.CallId);
                _dayPayment.Add(new CallPayment(_currentRate, rated.Rating));
            }
        }

        private void HourChanged(HourChanged hourChanged)
        {
            if (hourChanged.Hour == 20)
                WorkDayEnded();
        }

        private void WorkDayEnded()
        {
            _playerAccount.PaySalary(_dayPayment);
            World.Publish(new PlayerNotification("MegaBuy", $"You have been paid MBit - {_dayPayment.Amount()}"));
            _dayPayment = new DayPayment();
        }

        private void TechnicalMistakeOccurred(TechnicalMistakeOccurred mistake)
        {
            _dayPayment.Remove(mistake.PayPenalty);
            World.Publish(new PlayerNotification("MegaBuy", $"{mistake.PayPenalty.Amount()} MBit penalty. You violated policy: {mistake.Policy.Text}"));
        }
    }
}
