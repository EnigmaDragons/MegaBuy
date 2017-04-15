using System;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Calls.Events;
using MegaBuy.Temp;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Calls.Messages
{
    public class CallMessengerUI : IVisualAutomaton
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(Sizes.Margin, Sizes.Margin));
        private readonly List<MessageUI> _messages = new List<MessageUI>();
        private readonly List<MessageUI> _incomingMessages = new List<MessageUI>();
        private readonly Timer _timer;

        public CallMessengerUI()
        {
            _timer = new Timer(UpdateMessenger, 1000);
            World.Subscribe(EventSubscription.Create<CallStarted>(x => AddMessages(x.Call.Script), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => Clear(), this));
        }

        public void Update(TimeSpan delta)
        {
            _timer.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/Messenger", parentTransform + new Transform2(_transform.Location, Sizes.Messenger));
            var currentHeightUsed = 0;
            for (var i = 0; i < _messages.Count; i++)
            {
                if (currentHeightUsed + _messages[i].Height > Sizes.Messenger.Height - Sizes.MessageMargin * 2)
                    break;
                _messages[i].Draw(parentTransform + _transform +
                    new Transform2(new Vector2(Sizes.MessageMargin, Sizes.Messenger.Height - _messages[i].Height - Sizes.MessageMargin - currentHeightUsed)));
                currentHeightUsed += _messages[i].Height + Sizes.MessageMargin;
            }
        }

        private void AddMessages(Script script)
        {
            script.ForEach(y => AddMessage(y.Text, y.CharacterName.Equals("player", StringComparison.OrdinalIgnoreCase)));
        }

        private void AddMessage(string text, bool isPlayer)
        {
            _incomingMessages.Add(new MessageUI(text, Sizes.Messenger.Width - Sizes.MessageMargin * 2, isPlayer));
        }

        private void Clear()
        {
            _incomingMessages.Clear();
            _messages.Clear();
        }

        private void UpdateMessenger()
        {
            if (!_incomingMessages.Any())
                return;
            _messages.Insert(0, _incomingMessages.First());
            _incomingMessages.RemoveAt(0);
        }
    }
}
