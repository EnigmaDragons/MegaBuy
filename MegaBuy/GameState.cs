using System;
using System.Linq;
using MegaBuy.Apartment;
using MegaBuy.Calls;
using MegaBuy.Jobs.Referrer;
using MegaBuy.Money;
using MegaBuy.Save;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MegaBuy.Money.Accounts;
using MegaBuy.Pads;
using MegaBuy.Player;
using MegaBuy.Policies;
using MegaBuy.Rents;
using MonoDragons.Core.Common;
using MegaBuy.MegaBuyCorporation.JobRoles.Referrer;
using MegaBuy.MegaBuyCorporation;
using MegaBuy.MegaBuyCorporation.Policies;

namespace MegaBuy
{
    public class GameState
    {
        public string CharName { get; set; }
        public Clock Clock { get; set; }
        public PlayerCharacter PlayerCharacter { get; set; }
        public PlayerAccount PlayerAccount { get; set; }
        public Pad Pad { get; set; }
        public Map<Type, object> SingleInstanceSubscriptions { get; set; }
        public Landlord Landlord { get; set; }
        public ActivePolicies ActivePolicies { get; set; }

        public GameState()
        {
            CharName = "player";
            ActivePolicies = new ActivePolicies();
            ActivePolicies.Add(ReferrerPolicies.Level1Policies);
            Clock = new Clock(400, 8, 0);
            PlayerAccount = new PlayerAccount();
            SingleInstanceSubscriptions = new Map<Type, object>();
            Landlord = new Landlord(new Rent(50), PlayerAccount);
            AddSingleInstanceSubscription(new CallQueue());
            AddSingleInstanceSubscription(new MegaBuyEmployment(ActivePolicies));
            AddSingleInstanceSubscription(new MegaBuyAccounting(PlayerAccount, ReferrerPerCallRates.Level1PerCallRate));
            AddSingleInstanceSubscription(new GovernmentTaxes(PlayerAccount));
            AddSingleInstanceSubscription(new AutoSave());
            World.Publish(new DayStarted(0));
            //World.Publish(new TimeRateChanged(5.0f)); // To speed the game during development
        }

        private void AddSingleInstanceSubscription(object obj)
        {
            SingleInstanceSubscriptions.Add(obj.GetType(), obj);
        }

        public object GetSaveData()
        {
            return new
            {
                Clock = Clock.Time,
                PlayerAccount = PlayerAccount.Amount()
            };
        }
    }
}
