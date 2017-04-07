﻿using System;
using System.Collections.Generic;
using MegaBuy.Food;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Apps
{
    public sealed class FoodApp : IApp
    {
        private readonly ClickUI _clickUI;
        private readonly ClickUILayer _layer;

        public App Type => App.Food;
        public readonly List<IVisual> _visuals;

        public FoodApp(ClickUI clickUi)
        {
            _layer = new ClickUILayer("Food App") {Location = new Vector2(200, 100)};
            _visuals = new List<IVisual>();
            _visuals.Add(new FoodOptionUI(new FoodViewModel("PC", new FoodCost(1), "pancakes"), _layer, 0));
            _visuals.Add(new FoodOptionUI(new FoodViewModel("Cakes", new FoodCost(10), "pancakes"), _layer, 1));
            _visuals.Add(new FoodOptionUI(new FoodViewModel("Pancakes", new FoodCost(100), "pancakes"), _layer, 2));
            _visuals.Add(new FoodOptionUI(new FoodViewModel("Fancy Pancakes", new FoodCost(1000), "pancakes"), _layer, 3));
            _visuals.Add(new FoodOptionUI(new FoodViewModel("Peach Pancakes", new FoodCost(10000), "pancakes"), _layer, 4));
            _visuals.Add(new FoodOptionUI(new FoodViewModel("Blueberry Pancakes", new FoodCost(100000), "pancakes"), _layer, 5));
            _visuals.Add(new FoodOptionUI(new FoodViewModel("Chocolate Pancakes", new FoodCost(1000000), "pancakes"), _layer, 6));
            _visuals.Add(new FoodOptionUI(new FoodViewModel("Strawberry Pancakes", new FoodCost(10000000), "pancakes"), _layer, 7));
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
