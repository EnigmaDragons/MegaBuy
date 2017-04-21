using MegaBuy.Temp;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Pads
{
    public class TogglePad : IVisual
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(800 - Sizes.PadToggle.Width / 2, 900 - Sizes.SmallMargin - Sizes.PadToggle.Height));
        private readonly ImageTextButton _open;
        private readonly ImageTextButton _close;
        public ClickUIBranch Branch { get; set; }

        private IVisual _current;

        public TogglePad()
        {
            _open = ImageTextButtonFactory.CreateTrapazoid("Open", Vector2.Zero, () => World.Publish(new PadOpened()));
            _close = ImageTextButtonFactory.CreateTrapazoid("Close", Vector2.Zero, () => World.Publish(new PadClosed()));
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
