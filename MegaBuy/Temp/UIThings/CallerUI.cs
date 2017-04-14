﻿using System;
using System.Linq;
using MegaBuy.Calls;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Temp.UIThings
{
    public class CallerUI : IVisual
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(1350 - Sizes.Margin, Sizes.Margin));
        private readonly Label _name;
        
        private string _caller;
        private bool _isInCall = false;

        public CallerUI()
        {
            _name = new Label
            {
                BackgroundColor = Color.Transparent,
                Font = "Fonts/arial",
                TextColor = Color.Black,
                Transform = new Transform2(new Vector2(0, 610 + Sizes.Margin), new Size2(250, 50))
            };
            World.Subscribe(EventSubscription.Create<CallConnecting>(x => StartConnecting(), this));
            World.Subscribe(EventSubscription.Create<CallStarted>(x => StartCall(x.Call), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => EndCall(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            World.Draw("Images/UI/caller", absoluteTransform + new Transform2(new Size2(250, 610)));
            World.Draw("Images/UI/name", absoluteTransform + new Transform2(new Vector2(0, 610 + Sizes.Margin), new Size2(250, 50)));
            if (_isInCall)
            {
                World.Draw("Images/Customers/" + _caller, parentTransform + _transform + new Transform2(new Vector2(5, 5), new Size2(240, 400)));
                _name.Draw(absoluteTransform);
            }
            
        }

        private void StartConnecting()
        {
            
        }

        private void StartCall(Call call)
        {
            _isInCall = true;
            _caller = call.Script.First(x => !x.CharacterName.Equals("player", StringComparison.OrdinalIgnoreCase)).CharacterName;
            _name.Text = _caller;
        }

        private void EndCall()
        {
            _isInCall = false;
        }
    }
}
