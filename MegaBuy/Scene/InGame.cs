using System;
using MegaBuy.Apartment;
using MegaBuy.Apartment.Map;
using MegaBuy.Pads;
using MegaBuy.Player;
using MegaBuy.Player.Energy;
using MegaBuy.Player.Thoughts;
using MegaBuy.Temp;
using MegaBuy.Time;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Scene
{
    public class InGame : IScene
    {
        private readonly double _speed = 3.0;

        private readonly ClickUI _clickUi = new ClickUI();
        private readonly ClickUIBranch _branch = new ClickUIBranch("Game", (int)ClickUIPriorities.Base);
        private DevView _dev;
        private Overlay _overlay;
        private Pad _pad;
        private Clock _clock;
        private ThoughtUI _thoughts;
        private RoomUI _room;

        private bool _isSleeping;
        private bool _isPadOpen;
        private int _padLocation;
        private GameState _gameState;

        public void Init()
        {
            _gameState = CurrentGameState.StartNewGame();
            _padLocation = 900;
            _clock = _gameState.Clock;
            _clickUi.Add(_branch);
            _overlay = new Overlay();
            _branch.Add(_overlay.Branch);
            _pad = new Pad(_branch);
            _gameState.Pad = _pad;
            _room = new RoomUI();
            _thoughts = new ThoughtUI();
            _branch.Add(_thoughts.Branch);
            _dev = new DevView();
            _branch.Add(_dev.Branch);
            _branch.Add(_room.Branch);
            World.Subscribe(EventSubscription.Create<PadOpened>(x => _isPadOpen = true, this));
            World.Subscribe(EventSubscription.Create<PadClosed>(x => _isPadOpen = false, this));
            World.Subscribe(EventSubscription.Create<WentToBed>((e) => WentToBed(), this));
            World.Subscribe(EventSubscription.Create<Awaken>(Awaken, this));
            World.Subscribe(EventSubscription.Create<CollapsedWithExhaustion>((e) => WentToBed(), this));
        }

        private void Awaken(Awaken obj)
        {
            _isSleeping = false;
            _clickUi.Add(_branch);
        }

        private void WentToBed()
        {
            _isSleeping = true;
            _clickUi.Remove(_branch);
        }

        public void Update(TimeSpan delta)
        {
            _room.Update(delta);
            _padLocation = _isPadOpen
                ? (int) Math.Max(_padLocation - delta.TotalMilliseconds * _speed, 0)
                : (int) Math.Min(_padLocation + delta.TotalMilliseconds * _speed, 900);
            _clock.Update(delta);
            _clickUi.Update(delta);
            _overlay.Update(delta);
            _pad.Update(delta);
        }

        public void Draw()
        {
            _room.Draw(Transform2.Zero);
            if (_isSleeping)
                return;
            _pad.Draw(new Transform2(new Vector2(0, _padLocation)));
            _thoughts.Draw(Transform2.Zero);
            _overlay.Draw(Transform2.Zero);
            _dev.Draw(Transform2.Zero);
        }
    }
}
