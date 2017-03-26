using System;
using System.Collections.Generic;
using MegaBuy.Food;
using MegaBuy.Map;
using MegaBuy.Player;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Scene
{
    public class SlowlyStarving : IScene
    {
        private readonly Transform2 _camera = new Transform2(new Vector2(180, 0), Rotation2.Default, 2);
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly List<IAutomaton> _automatons = new List<IAutomaton>();

        public void Init()
        {
            var map = ApartmentMapFactory.Create();
            _automatons.Add(map);
            _visuals.Add(map);
            var player = new PlayerCharacter(map, new TileLocation(4, 4).Transform);
            _automatons.Add(player);
            _visuals.Add(player);
            var hungerUi = new HungerUI(new ImageBox(new Transform2(new Vector2(0, 0), new Size2(50, 50))));
            _visuals.Add(hungerUi);
        }

        public void Update(TimeSpan delta)
        {
            _automatons.ForEach(x => x.Update(delta));
        }

        public void Draw()
        {
            _visuals.ForEach(x => x.Draw(_camera));
        }
    }
}
