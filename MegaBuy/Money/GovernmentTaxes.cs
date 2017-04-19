using MegaBuy.Money.Accounts;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using System.Collections.Generic;
using System;
using MegaBuy.Money.Amounts;
using MegaBuy.Notifications;
using MonoDragons.Core.Common;

namespace MegaBuy.Money
{
    public class GovernmentTaxes
    {
        private readonly IAccount _playerAccount;

        public GovernmentTaxes(IAccount playerAccount)
        {
            _playerAccount = playerAccount;
            World.Subscribe(EventSubscription.Create<HourChanged>(HourChanged, this));
            World.Subscribe(EventSubscription.Create<SalaryPaymentOccured>(ApplyIncomeTaxes, this));
        }

        private void HourChanged(HourChanged obj)
        {
            if(Rng.Int(10) == 9)
            {
                _playerAccount.Remove(new Tax(10));
                World.Publish(new PlayerNotification("Government", $"You have automatically paid living taxes with MBit - 10"));
            }
        }

        private void ApplyIncomeTaxes(SalaryPaymentOccured obj)
        {
            _playerAccount.Remove(new Tax(obj.Amount.Amount() / 2));
            World.Publish(new PlayerNotification("Government", $"You have automatically paid income taxes with MBit - {obj.Amount.Amount() / 2}"));
        }
    }
}
