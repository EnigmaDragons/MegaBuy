using System;
using MegaBuy.Apartment;
using MegaBuy.Money;
using MegaBuy.Pads;
using MegaBuy.Player;
using MegaBuy.Temp;
using MegaBuy.Time;
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
        private OverlayUI _overlay;
        private Pad _pad;
        private Clock _clock;

        private bool _isPadVisible;
        private ApartmentMap _map;
        private PlayerCharacter _player;

        public void Init()
        {
            _clock = GameState.Clock;
            _clickUi.Add(_branch);
            _overlay = new OverlayUI(_branch);
            _pad = new Pad(_branch);
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
