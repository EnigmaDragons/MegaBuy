using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Apps
{
    public sealed class PAD : IVisualAutomaton
    {
        private readonly Map<App, IApp> _apps = new Map<App, IApp>();
        private readonly ClickUI _clickUi;
        private readonly MenuBar _menuBar;

        private IApp _currentApp;

        public PAD()
        {
            _currentApp = new NoneApp();
            _clickUi = new ClickUI();
            _menuBar = new MenuBar(_clickUi, this);
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
                return new CallApp(_clickUi);
            if (app.Equals(App.Food))
                return new FoodApp(_clickUi);
            if (app.Equals(App.Notification))
                return new NotificationApp(_clickUi);
            throw new KeyNotFoundException($"Unknown App Type {app}");
        }

        public void Update(TimeSpan delta)
        {
            _clickUi.Update(delta);
            _apps.ForEach(x => x.Value.Update(delta));
        }

        public void Draw(Transform2 parentTransform)
        {
            World.Draw("Images/PAD/background", new Transform2(new Vector2(0, 0), new Size2(1600, 900)));
            _menuBar.Draw(parentTransform);
            _currentApp?.Draw(new Transform2(new Vector2(200, 0), new Size2(0, 0)));
            _clickUi.Draw(Transform2.Zero);
        }
    }
}
