using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Calls.UIThings
{
    public class CallApp : IVisualAutomaton
    {
        public void Update(TimeSpan delta)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
            var appWidth = parentTransform.Size.Width;
            var appHeight = parentTransform.Size.Height;
            var appBackgroundColor = Color.FromNonPremultiplied(33, 33, 33, 100);

            var callerBoxWidth = appWidth/2 - 20;
            var callerBoxHeight = appHeight/2 - 20;
            var callerBoxColor = Color.FromNonPremultiplied(0, 0, 100, 150);
            var callerBoxOffsetX = 10;
            var callerBoxOffsetY = 10;

            var callerWidth = callerBoxWidth - 20;
            var callerHeight = callerBoxHeight - 20;
            var callerOffsetX = 20;
            var callerOffsetY = 20;

            var chatBoxWidth = appWidth/2 - 20;
            var chatBoxHeight = appHeight / 2 - 20;
            var chatBoxColor = Color.FromNonPremultiplied(00, 00, 00, 150);
            var chatBoxOffsetX = appWidth / 2 + 10;
            var chatBoxOffsetY = 10;

            World.Draw(new RectangleTexture(appWidth, appHeight, appBackgroundColor).Create(), parentTransform);
            World.Draw(new RectangleTexture(callerBoxWidth, callerBoxHeight, callerBoxColor).Create(), new Transform2(new Vector2(callerBoxOffsetX, callerBoxOffsetY)));
            World.Draw("Images/Screen/female-customer.png", new Transform2(new Vector2(callerOffsetX, callerOffsetY), Rotation2.Default, new Size2(callerWidth, callerHeight), 1f));
            World.Draw(new RectangleTexture(chatBoxWidth, chatBoxHeight, chatBoxColor).Create(), new Transform2(new Vector2(chatBoxOffsetX, chatBoxOffsetY)));
        }
    }
}
