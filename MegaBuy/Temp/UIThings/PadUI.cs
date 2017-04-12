using System;
using System.Collections.Generic;
using MegaBuy.Apps;
using MegaBuy.Temp.Events;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Temp
{
    public class PadUI : IVisualAutomaton
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(0, 0));
        private readonly MenuBarUI _menuBar;
        private readonly Map<App, IApp> _apps = new Map<App, IApp>();
        private readonly ClickUI _clickUI;
        private IApp _currentApp;

        public PadUI(ClickUI clickUI)
        {
            _clickUI = clickUI;
            _menuBar = new MenuBarUI(clickUI);
            _currentApp = new NoneApp();
            World.Subscribe(EventSubscription.Create<AppChanged>(x => OpenApp(x.App), this));
        }

        public void Update(TimeSpan delta)
        {
            _apps.ForEach(x => x.Value.Update(delta));
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/PAD/background", new Transform2(_transform.Location, new Size2(1600, 900)));
            _menuBar.Draw(parentTransform + _transform);
            _currentApp.Draw(parentTransform + _transform + new Transform2(new Vector2(0, 75)));
        }

        public void OpenApp(App app)
        {
            if (_currentApp.Type == app)
                return;

            if (!_apps.ContainsKey(app))
                _apps[app] = MakeApp(app);

            _currentApp.LostFocus();
            _currentApp = _apps[app];
            _apps[app].GainedFocus();
        }

        private IApp MakeApp(App app)
        {
            if (app.Equals(App.Call))
                return new CallApp(_clickUI);
            if (app.Equals(App.Food))
                return new FoodApp(_clickUI);
            if (app.Equals(App.Notification))
                return new NotificationApp(_clickUI);
            throw new KeyNotFoundException($"Unknown App Type {app}");
        }
    }
}
