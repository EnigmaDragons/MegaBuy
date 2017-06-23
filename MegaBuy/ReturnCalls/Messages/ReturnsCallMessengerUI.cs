using System;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Calls.Events;
using MegaBuy.Calls.Messages;
using MegaBuy.NewCalls.Messages;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.ReturnCalls.Messages
{
    public class ReturnsCallMessengerUI : IVisualAutomaton
    {
        private readonly List<ReturnMessageUI> _pastMessages = new List<ReturnMessageUI>();
        private readonly List<ReturnMessageUI> _messages = new List<ReturnMessageUI>();
        private readonly List<ReturnMessageUI> _incomingMessages = new List<ReturnMessageUI>();
        private readonly Timer _timer;
        private readonly Transform2 _transform;

        private Chat _chat = new Chat();
        private int _msg;
        private bool _isInCall = false;
        private int _messagesTotalHeight = 0;

        public ReturnsCallMessengerUI(Transform2 transform)
        {
            _timer = new Timer(UpdateMessenger, 1000);
            _transform = transform;
            World.Subscribe(EventSubscription.Create<CallStarted>(x => AddMessages(x.Call.Chat), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => Clear(), this));
        }

        public void Update(TimeSpan delta)
        {
            _timer.Update(delta);
            if (_isInCall)
                ProcessNewChatMessages();
        }

        private void ProcessNewChatMessages()
        {
            for (; _msg < _chat.Count; _msg++)
            {
                _incomingMessages.Insert(0, new ReturnMessageUI(_chat[_msg].Text, _transform.Size.Width - Sizes.MessageMargin * 2, _chat[_msg].Role.Equals(CallRole.Player)));
                UpdateMessenger();
            }
        }

        private void ProcessInitialChatMessages()
        {
            for (; _msg < _chat.Count; _msg++)
                AddInitialMessage(_chat[_msg].Text, _chat[_msg].Role.Equals(CallRole.Player));
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/Messenger", parentTransform + _transform);
            var currentHeightUsed = 0;
            for (var i = 0; i < _messages.Count; i++)
            {
                _messages[i].Draw(parentTransform + 
                    new Transform2(new Vector2(Sizes.MessageMargin, Sizes.MessageMargin + currentHeightUsed)));
                currentHeightUsed += _messages[i].Height + Sizes.MessageMargin;
            }
        }

        private void AddMessages(Chat chat)
        {
            _msg = 0;
            _chat = chat;
            ProcessInitialChatMessages();
            _isInCall = true;
        }

        private void AddInitialMessage(string text, bool isPlayer)
        {
            _incomingMessages.Add(new ReturnMessageUI(text, _transform.Size.Width - Sizes.MessageMargin * 2, isPlayer));
        }

        private void Clear()
        {
            _isInCall = false;
            _chat = new Chat();
            _messagesTotalHeight = 0;
            _pastMessages.Clear();
            _incomingMessages.Clear();
            _messages.Clear();
        }

        private void UpdateMessenger()
        {
            if (!_incomingMessages.Any())
                return;
            _messages.Add(_incomingMessages.First());
            _incomingMessages.RemoveAt(0);
            _messagesTotalHeight += _messages.Last().Height + Sizes.MessageMargin;
            while (_messagesTotalHeight > _transform.Size.Height - Sizes.MessageMargin*2)
            {
                _pastMessages.Add(_messages.First());
                _messages.RemoveAt(0);
                _messagesTotalHeight -= _pastMessages.Last().Height + Sizes.MessageMargin;
            }
        }
    }
}
