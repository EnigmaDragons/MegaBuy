﻿using MegaBuy.Money;

namespace MegaBuy.Food
{
    public class Food
    {
        public string Name { get; }
        public IAmount Cost { get; }
        public int HungerRecovery { get; }
        public string Image { get; }

        public Food(string name, IAmount cost, int hungerRecovery, string image)
        {
            Name = name;
            Cost = cost;
            HungerRecovery = hungerRecovery;
            Image = image;
        }
    }
}
