using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Apps
{
    public sealed class MenuBar : IVisual
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();

        public MenuBar(ClickUI clickUi, PAD pad)
        {
            var logoff = new ImageButton(
               "Images/Icons/poweroff",
               "Images/Icons/poweroff-hover",
               "Images/Icons/poweroff-hover",
               new Transform2(new Vector2(52, 750), new Size2(90, 90)),
               () => World.NavigateToScene("Room"));
            _visuals.Add(logoff);
            clickUi.Add(logoff);

            var iconRect = new Rectangle(new Point(55, 10), new Point(90, 90));
            var food = new IconButton("Images/Icons/burger", iconRect, new Rectangle(new Point(0, 0), new Point(200, 110)),
                Color.LightBlue, Color.Blue, Color.DarkBlue, () => pad.OpenApp(App.Food));
            _visuals.Add(food);
            clickUi.Add(food);

            var call = new IconButton("Images/Icons/video-call", iconRect, new Rectangle(new Point(0, 110), new Point(200, 110)),
                Color.LightBlue, Color.Blue, Color.DarkBlue, () => pad.OpenApp(App.Call));
            _visuals.Add(call);
            clickUi.Add(call);

            var notification = new IconButton("Images/Icons/notification", iconRect, new Rectangle(new Point(0, 220), new Point(200, 110)),
                Color.LightBlue, Color.Blue, Color.DarkBlue, () => pad.OpenApp(App.Notification));
            _visuals.Add(notification);
            clickUi.Add(notification);
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ForEach(x => x.Draw(parentTransform));
        }
    }
}
