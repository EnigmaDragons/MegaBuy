using MegaBuy.Temp;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.CustomUI
{
    public class TogglePadUI : IVisual
    {
        private readonly Vector2 _location = new Vector2(600, 850);
        private readonly SingleImageButton _open;
        private readonly SingleImageButton _close;
        private readonly ClickUILayer _layer;

        private IVisual _current;

        public TogglePadUI(ClickUILayer layer)
        {
            _open = new SingleImageButton("Images/UI/open", Colors.Hover, Colors.Press, new Transform2(new Size2(400, 50)), () => World.Publish(new PadOpened()));
            _close = new SingleImageButton("Images/UI/close", Colors.Hover, Colors.Press, new Transform2(new Size2(400, 50)), () => World.Publish(new PadClosed()));
            _layer = layer;
            _current = _open;
            layer.Add(_open);
            layer.Location = _location;
            World.Subscribe(new EventSubscription<PadOpened>(x => PadOpened(), this));
            World.Subscribe(new EventSubscription<PadClosed>(x => PadClosed(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            _current.Draw(parentTransform + new Transform2(_location));
        }

        private void PadOpened()
        {
            _current = _close;
            _layer.Remove(_open);
            _layer.Add(_close);
        }

        private void PadClosed()
        {
            _current = _open;
            _layer.Remove(_close);
            _layer.Add(_open);
        }
    }
}
