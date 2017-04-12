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
        private readonly ClickUI _clickUI;
        private readonly ClickUILayer _layer;

        private App _currentApp = App.None;

        public MenuBarUI(ClickUI clickUI)
        {
            _callApp = new ImageButton("Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press", 
                new Transform2(new Vector2(575 - Sizes.Margin, Sizes.Margin), Sizes.Button), () => ChangeApp(App.Call));
            _foodApp = new ImageButton("Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press", 
                new Transform2(new Vector2(725, Sizes.Margin), Sizes.Button), () => ChangeApp(App.Food));
            _notificationApp = new ImageButton("Images/UI/button", "Images/UI/button", "Images/UI/button", 
                new Transform2(new Vector2(875 + Sizes.Margin, Sizes.Margin), Sizes.Button), () => ChangeApp(App.Notification));
            _clickUI = clickUI;
            _layer = new ClickUILayer("Pad Menu");
            _layer.Add(_callApp);
            _layer.Add(_foodApp);
            _layer.Add(_notificationApp);

            //temp code that will go away
            World.Subscribe(EventSubscription.Create<PadOpened>(x => _clickUI.Add(_layer), this));
            World.Subscribe(EventSubscription.Create<PadClosed>(x => _clickUI.Remove(_layer), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            _callApp.Draw(parentTransform + _transform);
            _foodApp.Draw(parentTransform + _transform);
            _notificationApp.Draw(parentTransform + _transform);
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
