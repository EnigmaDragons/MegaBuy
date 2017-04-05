using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Apps
{
    public sealed class FoodApp : IApp
    {
        public App Type => App.Food;



        public void Update(TimeSpan delta)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
            World.DrawRectangle(new Rectangle(200, 0, 1400, 900), Color.Yellow);
        }
    }
}
