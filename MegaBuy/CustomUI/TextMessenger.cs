using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.CustomUI
{
    public sealed class TextMessenger : IVisual
    {
        private readonly List<Message> _messages = new List<Message>();
        private readonly List<Label> _labels = new List<Label>();

        private readonly int _maxMessages;
        private readonly Color _textColor;
        private readonly Size2 _labelSize;
        private readonly int _verticalMargin = 20;

        private int _currentIndex = 0;

        public TextMessenger(int maxMessages, Color textColor, Size2 labelSize)
        {
            _maxMessages = maxMessages;
            _textColor = textColor;
            _labelSize = labelSize;
        }

        public void AddMessage(string text, Color messageBackground)
        {
            _messages.Add(new Message {Text = text, Background = messageBackground});
            if (_messages.Count > _maxMessages)
                _currentIndex = _messages.Count - _maxMessages;
            UpdateLabels();
        }

        public void UpdateLabels()
        {
            if (_messages.Count > _labels.Count && _labels.Count < _maxMessages)
                _labels.Add(MakeLabel());
            var l = 0;
            for (var m = _currentIndex; l < _maxMessages && m < _messages.Count; m++, l++)
            {
                _labels[l].Text = _messages[m].Text;
                _labels[l].BackgroundColor = _messages[m].Background;
            }
        }

        private Label MakeLabel()
        {
            return new Label
            {
                Transform = new Transform2(_labelSize),
                Text = _messages.Last().Text ,
                BackgroundColor = _messages.Last().Background,
                TextColor = _textColor
            };
        }

        public void Draw(Transform2 parentTransform)
        {
            for (var i = 0; i < _labels.Count; i++)
                _labels[i].Draw(new Transform2(new Vector2(0, (_labelSize.Height + _verticalMargin) * i)));
        }

        private struct Message
        {
            public string Text { get; set; }
            public Color Background { get; set; }
        }
    }
}
