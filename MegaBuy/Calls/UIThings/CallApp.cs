using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Calls.UIThings
{
    public class CallApp : IVisualAutomaton
    {
        //1400
        //900
        private Size2 caller = new Size2(400, 450);
        private Size2 messengertext = new Size2(400, 450);
        private readonly List<IVisual> _visuals = new List<IVisual>();

        public CallApp(ClickUI ui)
        {
            var info = new ImageButton(
                "Images/PAD/logout-default",
                "Images/PAD/logout-hover",
                "Images/PAD/logout-pressed",
                new Transform2(new Vector2(300, 600), new Size2(200, 50)),
                () => { });
            var troubleshooting = new ImageButton(
                "Images/PAD/logout-default",
                "Images/PAD/logout-hover",
                "Images/PAD/logout-pressed",
                new Transform2(new Vector2(300, 400), new Size2(200, 50)),
                () => { });

            ui.Add(info);
            ui.Add(troubleshooting);
            _visuals.Add(info);
            _visuals.Add(troubleshooting);
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw(new RectangleTexture(1400, 900, Color.Red).Create(), parentTransform.Location);
            var callerTransform = new Transform2(parentTransform.Location, caller);
            World.Draw(new RectangleTexture(caller.Width, caller.Height, Color.Gray).Create(), callerTransform);
            World.Draw("Images/Screen/female-customer", callerTransform);
            World.Draw(new RectangleTexture(messengertext.Width, messengertext.Height, Color.Gray).Create(), new Transform2(new Vector2(parentTransform.Location.X + 900, parentTransform.Location.Y + 0), messengertext));
            _visuals.ForEach(x => x.Draw(parentTransform));
        }
    }
}
