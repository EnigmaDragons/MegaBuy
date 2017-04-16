using System.Collections.Generic;
using MegaBuy.Calls.Events;
using MegaBuy.Temp;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Calls.Options
{
    public class CallOptionsUI : IVisual
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(800 - Sizes.Button.Width - Sizes.Margin / 2, Sizes.Margin));
        private readonly List<ImageTextButton> _buttons = new List<ImageTextButton>();

        public ClickUIBranch Branch { get; private set; }

        public CallOptionsUI()
        {
            Branch = new ClickUIBranch("Call Options", (int)ClickUIPriorities.Pad);
            World.Subscribe(EventSubscription.Create<CallStarted>(x => StartCall(x.Call), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => EndCall(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            var absolutePosition = parentTransform + _transform;
            Branch.ParentLocation = absolutePosition.Location;
            _buttons.ForEach(x => x.Draw(absolutePosition));
        }

        private void StartCall(Call call)
        {
            for (var i = 0; i < call.Options.Count; i++)
            {
                var button = new ImageTextButton(call.Options[i].Description,
                    "Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press",
                    new Transform2(new Vector2(i % 2 * (Sizes.Button.Width + Sizes.Margin), (int)(i / 2) * (Sizes.Button.Height + Sizes.Margin)), Sizes.Button), call.Options[i].Go);
                _buttons.Add(button);
                Branch.Add(button);
            }
        }

        private void EndCall()
        {
            _buttons.ForEach(x => Branch.Remove(x));
            _buttons.Clear();
        }
    }
}
