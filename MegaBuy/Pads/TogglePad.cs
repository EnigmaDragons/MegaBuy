using MegaBuy.Temp;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Pads
{
    public class TogglePad : IVisual
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(600, 850));
        private readonly ImageButton _open;
        private readonly ImageButton _close;
        public ClickUIBranch Branch { get; set; }

        private IVisual _current;

        public TogglePad()
        {
            _open = new ImageButton("Images/UI/open", "Images/UI/open-hover", "Images/UI/open-press", 
                new Transform2(Sizes.PadToggle), () => World.Publish(new PadOpened()));
            _close = new ImageButton("Images/UI/close", "Images/UI/close-hover", "Images/UI/close-press", 
                new Transform2(Sizes.PadToggle), () => World.Publish(new PadClosed()));
            Branch = new ClickUIBranch("Toggle Pad", (int)ClickUIPriorities.Overlay);
            _current = _open;
            Branch.Add(_open);
            World.Subscribe(EventSubscription.Create<PadOpened>(x => PadOpened(), this));
            World.Subscribe(EventSubscription.Create<PadClosed>(x => PadClosed(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            Branch.ParentLocation = absoluteTransform.Location;
            _current.Draw(parentTransform + _transform);
        }

        private void PadOpened()
        {
            _current = _close;
            Branch.Remove(_open);
            Branch.Add(_close);
        }

        private void PadClosed()
        {
            _current = _open;
            Branch.Remove(_close);
            Branch.Add(_open);
        }
    }
}
