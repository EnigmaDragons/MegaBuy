using System;
using MegaBuy.CustomUI;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Scene
{
    public sealed class TextMessangerSample : IScene
    {
        private Timer _timer;
        private readonly TextMessenger _messenger = new TextMessenger(5, Color.Black, new Size2(500, 80));
        private bool _isPlayer;
        private int _message;

        public void Init()
        {
            _timer = new Timer(AddMessage, 2000);
        }

        public void AddMessage()
        {
            _message++;
            var color = _isPlayer ? Color.LightCoral : Color.LightBlue;
            _messenger.AddMessage(_message.ToString(), color);
            _isPlayer = !_isPlayer;
        }

        public void Update(TimeSpan delta)
        {
            _timer.Update(delta);
        }

        public void Draw()
        {
            _messenger.Draw(Transform2.Zero);
        }
    }
}
