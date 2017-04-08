using System;
using MegaBuy.Apps;
using MegaBuy.Calls;
using MegaBuy.Food;
using MegaBuy.Money;
using MegaBuy.Time;
using MonoDragons.Core.Engine;

namespace MegaBuy
{
    public static class GameState
    {
        public static Clock Clock { get; set; }
        public static PlayerAccount PlayerAccount { get; set; }
        public static PAD PAD { get; set; }
        public static Map<Type, object> SingleInstanceSubscriptions { get; set; }

        static GameState()
        {
            Clock = new Clock();
            PlayerAccount = new PlayerAccount();
            SingleInstanceSubscriptions = new Map<Type, object>();
            PAD = new PAD();
            AddSingleInstanceSubscription(new CallQueue());
            AddSingleInstanceSubscription(new MegaBuyAccounting(PlayerAccount));
            AddSingleInstanceSubscription(new FoodEmporium(PlayerAccount));
            World.Publish(new DayStarted(0));
        }

        private static void AddSingleInstanceSubscription(object obj)
        {
            SingleInstanceSubscriptions.Add(obj.GetType(), obj);
        }
    }
}
