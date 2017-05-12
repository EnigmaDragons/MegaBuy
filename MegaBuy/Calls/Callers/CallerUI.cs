using System;
using System.Linq;
using MegaBuy.Calls.Events;
using MegaBuy.Temp;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Calls.Callers
{
    public class CallerUi : IVisual
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(1350 - Sizes.Margin, Sizes.Margin));
        private readonly Label _info;
        
        private string _caller;
        private bool _isInCall = false;

        public CallerUi()
        {
            _info = new Label
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
                Transform = new Transform2(new Vector2(0, 400 + Sizes.Margin), new Size2(250, 260))
            };
            World.Subscribe(EventSubscription.Create<CallStarted>(x => StartCall(x.Call), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => EndCall(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            World.Draw("Images/UI/caller", absoluteTransform + new Transform2(new Size2(250, 410)));
            World.Draw("Images/UI/caller-info", absoluteTransform + new Transform2(new Vector2(0, 410 + Sizes.Margin), Sizes.CallerInfo));
            if (_isInCall)
            {
                World.Draw("Images/Customers/" + _caller, parentTransform + _transform + new Transform2(new Vector2(5, 5), new Size2(240, 400)));
                _info.Draw(absoluteTransform);
            }
        }

        private void StartCall(Call call)
        {
            _isInCall = true;
            _info.Text = "Name: " + call.Caller.Name + "\n" + string.Join("", call.Caller.Traits.Select(x => x.Key + ": " + x.Value + "\n"));
            _caller = call.Caller.Name.ToLower().Replace(" ", "-");
        }

        private void EndCall()
        {
            _isInCall = false;
        }
    }
}
