using MegaBuy.Calls;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Temp.UIThings
{
    public class ReadyUI : IVisual
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(725, 350));
        private readonly ImageButton _button;

        private bool _isCalling;

        public ClickUIBranch Branch { get; private set; }

        public ReadyUI()
        {
            Branch = new ClickUIBranch("Ready Button", (int)ClickUIPriorities.Pad);
            _button = new ImageButton("Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press",
                new Transform2(Sizes.Button), StartCall);
            Branch.Add(_button);
            World.Subscribe(EventSubscription.Create<CallResolved>(x => EndCall(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            Branch.ParentLocation = absoluteTransform.Location;
            if (_isCalling)
                _button.Draw(absoluteTransform);
        }

        private void StartCall()
        {
            _isCalling = true;
            World.Publish(new AgentCallStatusChanged(AgentCallStatus.Available));
        }

        private void EndCall()
        {
            _isCalling = false;
        }
    }
}
