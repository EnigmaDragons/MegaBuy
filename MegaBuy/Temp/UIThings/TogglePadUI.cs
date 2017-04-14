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
        private readonly ClickUIBranch _branch;

        private IVisual _current;

        public TogglePadUI(ClickUIBranch parentBranch)
        {
            _open = new ImageButton("Images/UI/open", "Images/UI/open-hover", "Images/UI/open-press", 
                new Transform2(Sizes.PadToggle), () => World.Publish(new PadOpened()));
            _close = new ImageButton("Images/UI/close", "Images/UI/close-hover", "Images/UI/close-press", 
                new Transform2(Sizes.PadToggle), () => World.Publish(new PadClosed()));
            _branch = new ClickUIBranch("Toggle Pad", (int)ClickUIPriorities.Overlay);
            parentBranch.Add(_branch);
            _current = _open;
            _branch.Add(_open);
            World.Subscribe(EventSubscription.Create<PadOpened>(x => PadOpened(), this));
            World.Subscribe(EventSubscription.Create<PadClosed>(x => PadClosed(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            _branch.ParentLocation = absoluteTransform.Location;
            _current.Draw(parentTransform + _transform);
        }

        private void PadOpened()
        {
            _current = _close;
            _branch.Remove(_open);
            _branch.Add(_close);
        }

        private void PadClosed()
        {
            _current = _open;
            _branch.Remove(_close);
            _branch.Add(_open);
        }
    }
}
