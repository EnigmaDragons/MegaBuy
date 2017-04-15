﻿using System;
using MegaBuy.Apartment;
using MegaBuy.Calls;
using MegaBuy.Foods;
using MegaBuy.Money;
using MegaBuy.Save;
using MegaBuy.Temp;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MegaBuy.Money.Accounts;
using MegaBuy.Pads;
using MegaBuy.Player;
using MegaBuy.Rents;

namespace MegaBuy
{
    public static class GameState
    {
        public static string CharName { get; set; }
        public static Clock Clock { get; set; }
        public static PlayerCharacter PlayerCharacter { get; set; }
        public static PlayerAccount PlayerAccount { get; set; }
        public static Pad Pad { get; set; }
        public static Map<Type, object> SingleInstanceSubscriptions { get; set; }
        public static Landlord Landlord { get; set; }

        static GameState()
        {
            CharName = "player";
            Clock = new Clock();
            PlayerAccount = new PlayerAccount();
            SingleInstanceSubscriptions = new Map<Type, object>();
            Landlord = new Landlord(new Rent(50), PlayerAccount);
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
