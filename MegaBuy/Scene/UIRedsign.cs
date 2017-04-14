using System;
using MegaBuy.Apartment;
using MegaBuy.CustomUI;
using MegaBuy.Map;
using MegaBuy.Money;
using MegaBuy.Player;
using MegaBuy.Temp;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Scene
{
    public class UIRedsign : IScene
    {
        private ClickUI _clickUi = new ClickUI();
        private OverlayUI _overlay;
        private PadUI _pad;
        private Clock _clock;

        private bool _isPadVisible;
        private ApartmentMap _map;
        private PlayerCharacter _player;

        public void Init()
        {
            _map = new ApartmentMap();
            _map.Add(new TileWalker(1, 13, 1, 13).Get(x => new Tile("floor", x, false, 0)));

            _map.Add(new Tile("bed-top", new TileLocation(12, 5), true, 1));
            _map.Add(new Tile("bed-mid", new TileLocation(12, 6), true, 1));
            _map.Add(new Tile("bed-bot", new TileLocation(12, 7), true, 1));

            _map.Add(new TileWalker(0, 1, 1, 12).Get(x => new Tile("wall-left", x, true, 1)));
            _map.Add(new TileWalker(13, 1, 1, 12).Get(x => new Tile("wall-right", x, true, 1)));
            _map.Add(new TileWalker(1, 12, 0, 1).Get(x => new Tile("wall-top", x, true, 1)));
            _map.Add(new TileWalker(1, 12, 13, 1).Get(x => new Tile("wall-bot", x, true, 1)));
            _map.Add(new Tile("wall-top-left", new TileLocation(0, 0), true, 1));
            _map.Add(new Tile("wall-top-right", new TileLocation(13, 0), true, 1));
            _map.Add(new Tile("wall-bot-left", new TileLocation(0, 13), true, 1));
            _map.Add(new Tile("wall-bot-right", new TileLocation(13, 13), true, 1));

            _map.Add(new Tile("desk1", new TileLocation(2, 11), true, 1));
            _map.Add(new Tile("desk2", new TileLocation(3, 11), true, 1));
            _map.Add(new Tile("desk3", new TileLocation(4, 11), true, 1));
            _map.Add(new Tile("desk4", new TileLocation(2, 12), true, 1));
            _map.Add(new Tile("desk5", new TileLocation(3, 12), true, 1));
            _map.Add(new Tile("desk6", new TileLocation(4, 12), true, 1));

            _player = new PlayerCharacter(_map, new TileLocation(11, 6).Transform);
            
            var layer = new ClickUIBranch("overlay", 3);
            _clock = GameState.Clock;
            _clickUi.Add(layer);
            _overlay = new OverlayUI(layer);
            _pad = new PadUI(_clickUi);
            GameState.Pad = _pad;
            World.Subscribe(EventSubscription.Create<PadOpened>(x => _isPadVisible = true, this));
            World.Subscribe(EventSubscription.Create<PadClosed>(x => _isPadVisible = false, this));
        }

        public void Update(TimeSpan delta)
        {
            _clock.Update(delta);
            _clickUi.Update(delta);
            _overlay.Update(delta);
            _pad.Update(delta);
        }

        public void Draw()
        {
            if(_isPadVisible)
                _pad.Draw(Transform2.Zero);
            _overlay.Draw(Transform2.Zero);
        }
    }
}
