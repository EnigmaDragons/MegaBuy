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

        public void Init()
        {
            _clock = GameState.Clock;
            var layer = new ClickUILayer("overlay");
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
