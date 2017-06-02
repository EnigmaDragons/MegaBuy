using System;
using MegaBuy.Apartment;
using MegaBuy.Calls;
using MegaBuy.Jobs;
using MegaBuy.Jobs.ReturnSpecialist;
using MegaBuy.Money;
using MegaBuy.Save;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MegaBuy.Money.Accounts;
using MegaBuy.Pads;
using MegaBuy.Player;
using MegaBuy.Rents;
using MegaBuy.MegaBuyCorporation;
using MegaBuy.MegaBuyCorporation.Policies;
using MegaBuy.Shopping;

namespace MegaBuy
{
    public class GameState
    {
        public DateTime DateTime => Clock.DateTime;
        public string CharName { get; set; }
        public Clock Clock { get; set; }
        public PlayerCharacter PlayerCharacter { get; set; }
        public PlayerAccount PlayerAccount { get; set; }
        public Pad Pad { get; set; }
        public Map<Type, object> SingleInstanceSubscriptions { get; set; }
        public Landlord Landlord { get; set; }
        public ActivePolicies ActivePolicies { get; set; }
        public Job Job { get; set; }

        public GameState()
        {
            Job = Job.ReturnSpecialistLevel1;
            CharName = "player";
            ActivePolicies = new ActivePolicies(ReturnSpecialistPolicies.Level1);
            Clock = new Clock(400, new DateTime(2328, 7, 16, 8, 0, 0));
            PlayerAccount = new PlayerAccount();
            SingleInstanceSubscriptions = new Map<Type, object>();
            Landlord = new Landlord(new Rent(50), PlayerAccount);
            AddSingleInstanceSubscription(new MegaBuyAccounting(PlayerAccount, ReturnSpecialistPerCallRates.Level1PerCallRate));
            AddSingleInstanceSubscription(new CallQueue());
            AddSingleInstanceSubscription(new MegaBuyEmployment(ActivePolicies));
            AddSingleInstanceSubscription(new GovernmentTaxes(PlayerAccount));
            AddSingleInstanceSubscription(new AutoSave());
            AddSingleInstanceSubscription(new FoodDelivery());
            World.Publish(new DayStarted(new DateTime(2328, 7, 16)));
            World.Publish(new JobChanged(Job));
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
