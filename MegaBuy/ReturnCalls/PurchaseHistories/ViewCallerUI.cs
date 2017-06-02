using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaBuy.Calls.Events;
using MegaBuy.ReturnCalls.Callers;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.ReturnCalls.PurchaseHistories
{
    public class ViewCallerUI : ISpatialVisualControl
    {
        private readonly ImageTextButton _button;

        public Transform2 Transform => _button.Transform;
        public ClickUIBranch Branch { get; }

        public ViewCallerUI()
        {
            Branch = new ClickUIBranch("View Caller Button", (int)ClickUIPriorities.Pad);
            _button = ImageTextButtonFactory.Create("Caller", Vector2.Zero, () => World.Publish(new CallerViewed()));
            Branch.Add(_button);
        }

        public void Draw(Transform2 parentTransform)
        {
            Branch.ParentLocation = parentTransform.Location;
            _button.Draw(parentTransform);
        }
    }
}
