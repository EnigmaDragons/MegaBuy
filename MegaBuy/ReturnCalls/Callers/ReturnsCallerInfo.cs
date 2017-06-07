using System.Linq;
using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.ReturnCalls.Callers
{
    public class ReturnsCallerInfo : IVisual
    {
        private readonly Transform2 _transform;
        private readonly Label _info;

        public ReturnsCallerInfo(Transform2 transform)
        {
            _transform = transform;
            _info = new Label
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
                HorizontalAlignment = HorizontalAlignment.Left,
                Transform = _transform + new Transform2(new Vector2(Sizes.SmallMargin), new Size2(-Sizes.SmallMargin * 2, -Sizes.SmallMargin * 2))
            };
            World.Subscribe(EventSubscription.Create<CallStarted>(x => OnCallStart(x.Call), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => OnCallResolved(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/UI/caller-info", parentTransform + _transform);
            _info.Draw(parentTransform);
        }

        private void OnCallStart(Call call)
        {
            _info.Text = "Name: " + call.Caller.Name + "\n" + string.Join("", call.Caller.Traits.Select(x => x.Key + ": " + x.Value + "\n"));
        }

        private void OnCallResolved()
        {
            _info.Text = "";
        }
    }
}
