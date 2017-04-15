using MegaBuy.Calls.Events;
using MegaBuy.Temp;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Calls
{
    public class ReadyUI : IVisual
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(725, 350));
        private readonly ImageTextButton _button;

        public ClickUIBranch Branch { get; private set; }

        public ReadyUI()
        {
            Branch = new ClickUIBranch("Ready Button", (int)ClickUIPriorities.Pad);
            _button = new ImageTextButton("Ready",
                "Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press",
                new Transform2(Sizes.Button), StartCall);
            Branch.Add(_button);
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            Branch.ParentLocation = absoluteTransform.Location;
            _button.Draw(absoluteTransform);
        }

        private void StartCall()
        {
            World.Publish(new AgentCallStatusChanged(AgentCallStatus.Available));
        }
    }
}
