using MegaBuy.Apps;
using MegaBuy.CustomUI;
using MegaBuy.Temp.Events;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Temp
{
    public class MenuBarUI : IVisual
    {
        private readonly Transform2 _transform = Transform2.Zero;
        private readonly ImageButton _callApp;
        private readonly ImageButton _foodApp;
        private readonly ImageButton _notificationApp;
        private readonly ImageButton _rentApp;
        private readonly ClickUIBranch _branch;

        private App _currentApp = App.None;

        public MenuBarUI(ClickUIBranch parentBranch)
        {
            _branch = new ClickUIBranch("Menu Bar", (int)ClickUIPriorities.Pad);
            parentBranch.Add(_branch);
            _callApp = new ImageButton("Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press", 
                new Transform2(new Vector2(575 - Sizes.Margin, Sizes.Margin), Sizes.Button), () => ChangeApp(App.Call));
            _foodApp = new ImageButton("Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press", 
                new Transform2(new Vector2(725, Sizes.Margin), Sizes.Button), () => ChangeApp(App.Food));
            _notificationApp = new ImageButton("Images/UI/button", "Images/UI/button", "Images/UI/button", 
                new Transform2(new Vector2(875 + Sizes.Margin, Sizes.Margin), Sizes.Button), () => ChangeApp(App.Notification));
            _rentApp = new ImageButton("Images/UI/button", "Images/UI/button", "Images/UI/button",
                new Transform2(new Vector2(1025 + Sizes.Margin, Sizes.Margin), Sizes.Button), () => ChangeApp(App.Rent));
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
