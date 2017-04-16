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
        private readonly ClickUIBranch _branch;

        private App _currentApp = App.None;

        public MenuBar(ClickUIBranch parentBranch)
        {
            _branch = new ClickUIBranch("Menu Bar", (int)ClickUIPriorities.Pad);
            parentBranch.Add(_branch);
            _callApp = new ImageTextButton("Calls",
                "Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press", 
                new Transform2(new Vector2(Sizes.Margin, Sizes.Margin), Sizes.Button), () => ChangeApp(App.Call));
            _foodApp = new ImageTextButton("Food",
                "Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press", 
                new Transform2(new Vector2(Sizes.Margin * 2 + Sizes.Button.Width, Sizes.Margin), Sizes.Button), () => ChangeApp(App.Food));
            _notificationApp = new ImageTextButton("Notification",
                "Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press",
                new Transform2(new Vector2(Sizes.Margin * 3 + Sizes.Button.Width * 2, Sizes.Margin), Sizes.Button), () => ChangeApp(App.Notification));
            _rentApp = new ImageTextButton("Rent",
                "Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press",
                new Transform2(new Vector2(Sizes.Margin * 4 + Sizes.Button.Width * 3, Sizes.Margin), Sizes.Button), () => ChangeApp(App.Rent));
            _branch.Add(_callApp);
            _branch.Add(_foodApp);
            _branch.Add(_notificationApp);
            _branch.Add(_rentApp);
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            _branch.ParentLocation = absoluteTransform.Location;
            _callApp.Draw(absoluteTransform);
            _foodApp.Draw(absoluteTransform);
            _notificationApp.Draw(absoluteTransform);
            _rentApp.Draw(absoluteTransform);
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
