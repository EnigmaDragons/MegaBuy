using System;
using System.Collections.Generic;
using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.UserInterface.Layouts;

namespace MegaBuy.ReturnCalls.Choices
{
    public class ChoicesUI : IVisualControl
    {
        private readonly GridLayout _grid;
        private readonly List<SmartControl> _choices = new List<SmartControl>();

        public ClickUIBranch Branch { get; }
        public Transform2 Transform { get; }

        public ChoicesUI(Transform2 transform)
        {
            Branch = new ClickUIBranch("Choices", (int)ClickUIPriorities.Pad);
            Transform = transform;
            _grid = new GridLayout(transform.Size, 2, 2);
            World.Subscribe(EventSubscription.Create<CallStarted>(x => OnCallStart(x.Call), this));
            World.Subscribe(EventSubscription.Create<CallResolved>(x => OnCallResolved(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            _grid.Draw(parentTransform);
        }

        public void OnCallStart(Call call)
        {
            for (int i = 0; i < call.Options.Count; i++)
            {
                var button = ImageTextButtonFactory.Create(call.Options[i].Description, Vector2.Zero, call.Options[i].Go);
                var smartButton = new SmartControl(button, (int)ClickUIPriorities.Pad);
                _choices.Add(smartButton);
                _grid.AddSpatial(smartButton, button.Transform, (i % 2) + 1, (int)Math.Floor((decimal)i / 2) + 1);
                Branch.Add(smartButton.Branch);
            }
        }

        public void OnCallResolved()
        {
            _choices.ForEach(x => _grid.Remove(x));
            _choices.Clear();
            Branch.ClearElements();
        }
    }
}
