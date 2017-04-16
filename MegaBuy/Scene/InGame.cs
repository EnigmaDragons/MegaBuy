using System;
using System.Drawing.Design;
using MegaBuy.Apartment;
using MegaBuy.Money;
using MegaBuy.Pads;
using MegaBuy.Player;
using MegaBuy.Temp;
using MegaBuy.Time;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Scene
{
    public class InGame : IScene
    {
        private ClickUI _clickUi = new ClickUI();
        private ClickUIBranch _branch = new ClickUIBranch("Game", (int)ClickUIPriorities.Base);
        private Overlay _overlay;
        private Pad _pad;
        private Clock _clock;

        private bool _isPadOpen;
        private ApartmentMap _map;
        private PlayerCharacter _player;
        private int _padLocation;

        public void Init()
        {
            _padLocation = 900;
            _clock = GameState.Clock;
            _clickUi.Add(_branch);
            _overlay = new Overlay();
            _branch.Add(_overlay.Branch);
            _pad = new Pad(_branch);
            GameState.Pad = _pad;
            World.Subscribe(EventSubscription.Create<PadOpened>(x => _isPadOpen = true, this));
            World.Subscribe(EventSubscription.Create<PadClosed>(x => _isPadOpen = false, this));
        }

        public void Update(TimeSpan delta)
        {
            _padLocation = _isPadOpen
                ? (int) Math.Max(_padLocation - delta.TotalMilliseconds, 0)
                : (int) Math.Min(_padLocation + delta.TotalMilliseconds, 900);
            _clock.Update(delta);
            _clickUi.Update(delta);
            _overlay.Update(delta);
            _pad.Update(delta);
        }

        public void Draw()
        {
            _pad.Draw(new Transform2(new Vector2(0, _padLocation)));
            _overlay.Draw(Transform2.Zero);
        }
    }
}
