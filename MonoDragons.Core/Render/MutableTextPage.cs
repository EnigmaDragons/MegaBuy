using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Render
{
    public sealed class MutableTextPage : IVisual
    {
        private readonly List<MutableDrawnText> _drawnTexts = new List<MutableDrawnText>();
        private readonly Transform2 _betweenTexts;
        private readonly Color _textColor;

        public MutableTextPage()
        {
            _betweenTexts = new Transform2(new Vector2(0, 100));
            _textColor = Color.White;
        }

        public void Set(List<string> texts)
        {
            while (_drawnTexts.Count < texts.Count)
                _drawnTexts.Add(new MutableDrawnText(_textColor));
            for (var i = 0; i < texts.Count; i++)
                _drawnTexts[i].Set(texts[i]);
        }

        public void Draw(Transform2 parentTransform)
        {
            var currentTransform = parentTransform;
            foreach (var text in _drawnTexts)
            {
                text.Draw(currentTransform);
                currentTransform += _betweenTexts;
            }
        }
    }
}
