using MegaBuy.Pads.Apps;
using MegaBuy.Temp;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Pads
{
    public class MenuBar : IVisual
    {
        private readonly Transform2 _transform = Transform2.Zero;
        private readonly ImageTextButton _callApp;
        private readonly ImageTextButton _foodApp;
        private readonly ImageTextButton _notificationApp;
        private readonly ImageTextButton _rentApp;
        private readonly ImageTextButton _policiesApp;
        private readonly ClickUIBranch _branch;

        private App _currentApp = App.None;

        public MenuBar(ClickUIBranch parentBranch)
        {
            _branch = new ClickUIBranch("Menu Bar", (int)ClickUIPriorities.Pad);
            parentBranch.Add(_branch);
            _callApp = ImageTextButtonFactory.Create("Calls", new Vector2(Sizes.Margin, Sizes.Margin), () => ChangeApp(App.Call));
            _foodApp = ImageTextButtonFactory.Create("Food", new Vector2(Sizes.Margin * 2 + Sizes.Button.Width, Sizes.Margin), () => ChangeApp(App.Food));
            _notificationApp = ImageTextButtonFactory.Create("Notification", new Vector2(Sizes.Margin * 3 + Sizes.Button.Width * 2, Sizes.Margin), () => ChangeApp(App.Notification));
            _rentApp = ImageTextButtonFactory.Create("Rent", new Vector2(Sizes.Margin * 4 + Sizes.Button.Width * 3, Sizes.Margin), () => ChangeApp(App.Rent));
            _policiesApp = ImageTextButtonFactory.Create("Policies", new Vector2(Sizes.Margin * 5 + Sizes.Button.Width * 4, Sizes.Margin), () => ChangeApp(App.Policies));
            _branch.Add(_callApp);
            _branch.Add(_foodApp);
            _branch.Add(_notificationApp);
            _branch.Add(_rentApp);
            _branch.Add(_policiesApp);
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            _branch.ParentLocation = absoluteTransform.Location;
            _callApp.Draw(absoluteTransform);
            _foodApp.Draw(absoluteTransform);
            _notificationApp.Draw(absoluteTransform);
            _rentApp.Draw(absoluteTransform);
            _policiesApp.Draw(absoluteTransform);
            World.Draw("Images/UI/line-vertical", absoluteTransform + 
                new Transform2(new Vector2(0, (int)(Sizes.Margin * 1.5 - Sizes.VerticalLine.Height / 2 + Sizes.Button.Height)), Sizes.VerticalLine));
        }

        private void ChangeApp(App app)
        {
            if (_currentApp == app)
                return;
            _currentApp = app;
            World.Publish(new AppChanged(app));
        }
    }
}
