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
using MegaBuy.Notifications;
using MegaBuy.Shopping;
using MegaBuy.Sounds;

namespace MegaBuy
{
    public class GameState : IDisposable
    {
        public Clock Clock { get; set; }
        public DateTime DateTime => Clock.DateTime;

        public string CharName { get; set; }
        public CharacterSex CharacterSex { get; set; }

        public PlayerCharacter PlayerCharacter { get; set; }
        public PlayerAccount PlayerAccount { get; set; }
        public Pad Pad { get; set; }
        public Map<Type, object> SingleInstanceSubscriptions { get; set; }
        public Landlord Landlord { get; set; }
        public ActivePolicies ActivePolicies { get; set; }
        public Mailbox Mailbox { get; set; }
        public Job Job { get; set; }

        public GameState(string charName, CharacterSex charSex)
        {
            Job = Job.ReturnSpecialistLevel1;
            CharName = charName;
            CharacterSex = charSex;
            ActivePolicies = new ActivePolicies(ReturnSpecialistPolicies.Level1);
            Clock = new Clock(400, new DateTime(2328, 7, 16, 8, 0, 0));
            PlayerAccount = new PlayerAccount();
            SingleInstanceSubscriptions = new Map<Type, object>();
            Landlord = new Landlord(new Rent(50), PlayerAccount);
            Mailbox = new Mailbox();
            AddSingleInstanceSubscription(new MegaBuyAccounting(PlayerAccount));
            AddSingleInstanceSubscription(new CallQueue());
            AddSingleInstanceSubscription(new MegaBuyEmployment(ActivePolicies));
            AddSingleInstanceSubscription(new GovernmentTaxes(PlayerAccount));
            AddSingleInstanceSubscription(new AutoSave());
            AddSingleInstanceSubscription(new FoodDelivery());
            AddSingleInstanceSubscription(new MegaBuyPolicyDepartment(ActivePolicies));
            AddSingleInstanceSubscription(new AllSounds());
            AddSingleInstanceSubscription(new MegaBuyReporting());
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

        public void Dispose()
        {
            foreach (var v in SingleInstanceSubscriptions.Values)
                World.Unsubscribe(v);
        }
    }
}
