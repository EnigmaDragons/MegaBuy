using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.CustomUI
{
    public class AutoSizingTextMessenger : IVisual
    {
        private readonly List<Message> _messages = new List<Message>();
        private readonly List<Label> _labels = new List<Label>();

        private readonly int _maxMessages;
        private readonly Color _textColor;
        private readonly int _verticalMargin = 20;

        private int _currentIndex = 0;

        public AutoSizingTextMessenger(int maxMessages, Color textColor)
        {
            _maxMessages = maxMessages;
            _textColor = textColor;
        }

        public void AddMessage(string text, Color messageBackground)
        {
            _messages.Add(new Message { Text = text, Background = messageBackground });
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
            var label = new Label
            {
                Text = _messages.Last().Text,
                BackgroundColor = _messages.Last().Background,
                TextColor = _textColor
            };
            var size = Resources.Load<SpriteFont>(label.Font).MeasureString(label.Text);
            label.Transform = new Transform2(new Size2((int)size.X + 10, (int)size.Y + 10));
            return label;
        }

        public void Draw(Transform2 parentTransform)
        {
            for (var i = 0; i < _labels.Count; i++)
                _labels[i].Draw(parentTransform + new Transform2(new Vector2(0, (_labels[i].Transform.Size.Height + _verticalMargin) * i)));
        }

        private struct Message
        {
            public string Text { get; set; }
            public Color Background { get; set; }
        }
    }
}
