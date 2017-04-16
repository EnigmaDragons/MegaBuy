﻿using System;
using System.Collections.Generic;
using MegaBuy.Pads.Apps;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Foods.Old
{
    public sealed class FoodApp : IApp
    {
        public ClickUIBranch Branch { get; }
        private readonly ClickUI _clickUI;
        private readonly ClickUIBranch _layer;

        public App Type => App.Food;
        public readonly List<IVisual> _visuals;

        public FoodApp(ClickUI clickUi)
        {
            _layer = new ClickUIBranch("Food App", 10) {Location = new Vector2(200, 100)};
            _visuals = new List<IVisual>();
            _visuals.Add(new FoodOptionUI(Menu.FoodMenu["Burger"], _layer, 0));
            _visuals.Add(new FoodOptionUI(new Food("Cakes", new FoodCost(10), 0, "pancakes"), _layer, 1));
            _visuals.Add(new FoodOptionUI(new Food("Pancakes", new FoodCost(100), 0, "pancakes"), _layer, 2));
            _visuals.Add(new FoodOptionUI(new Food("Fancy Pancakes", new FoodCost(1000), 0, "pancakes"), _layer, 3));
            _visuals.Add(new FoodOptionUI(new Food("Peach Pancakes", new FoodCost(10000), 0, "pancakes"), _layer, 4));
            _visuals.Add(new FoodOptionUI(new Food("Blueberry Pancakes", new FoodCost(100000), 0, "pancakes"), _layer, 5));
            _visuals.Add(new FoodOptionUI(new Food("Chocolate Pancakes", new FoodCost(1000000), 0, "pancakes"), _layer, 6));
            _visuals.Add(new FoodOptionUI(new Food("Strawberry Pancakes", new FoodCost(10000000), 0, "pancakes"), _layer, 7));
            _clickUI = clickUi;
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ForEach(x => x.Draw(new Transform2(new Vector2(200, 100))));
        }

        public void LostFocus()
        {
            _clickUI.Remove(_layer);
        }

        public void GainedFocus()
        {
            _clickUI.Add(_layer);
        }
    }
}