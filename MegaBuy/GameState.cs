﻿using System;
using MegaBuy.Apartment;
using MegaBuy.Apps;
using MegaBuy.Calls;
using MegaBuy.Food;
using MegaBuy.Money;
using MegaBuy.Save;
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
        public static Landlord Landlord { get; set; }

        static GameState()
        {
            Clock = new Clock();
            PlayerAccount = new PlayerAccount();
            SingleInstanceSubscriptions = new Map<Type, object>();
            PAD = new PAD();
            Landlord = new Landlord(new Rent(50));
            AddSingleInstanceSubscription(new CallQueue());
            AddSingleInstanceSubscription(new MegaBuyAccounting(PlayerAccount));
            AddSingleInstanceSubscription(new FoodEmporium(PlayerAccount));
            AddSingleInstanceSubscription(new AutoSave());
            World.Publish(new DayStarted(0));
        }

        private static void AddSingleInstanceSubscription(object obj)
        {
            SingleInstanceSubscriptions.Add(obj.GetType(), obj);
        }

        public static object GetSaveData()
        {
            return new
            {
                Clock = Clock.Time,
                PlayerAccount = PlayerAccount.Amount()
            };
        }
    }
}
