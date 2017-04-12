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
        private readonly Transform2 _transform = new Transform2(new Vector2(600, 850));
        private readonly ImageButton _open;
        private readonly ImageButton _close;
        private readonly ClickUILayer _layer;

        private IVisual _current;

        public TogglePadUI(ClickUILayer layer)
        {
            _open = new ImageButton("Images/UI/open", "Images/UI/open-hover", "Images/UI/open-press", 
                new Transform2(Sizes.PadToggle), () => World.Publish(new PadOpened()));
            _close = new ImageButton("Images/UI/close", "Images/UI/close-hover", "Images/UI/close-press", 
                new Transform2(Sizes.PadToggle), () => World.Publish(new PadClosed()));
            _layer = layer;
            _current = _open;
            layer.Add(_open);
            layer.Location = _transform.Location;
            World.Subscribe(new EventSubscription<PadOpened>(x => PadOpened(), this));
            World.Subscribe(new EventSubscription<PadClosed>(x => PadClosed(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            _current.Draw(parentTransform + _transform);
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
