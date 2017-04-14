using System;
using System.Collections.Generic;
using MegaBuy.Apps;
using MegaBuy.Temp.Events;
using MegaBuy.Temp.UIThings;
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
        private readonly ClickUIBranch _branch;
        private readonly MenuBarUI _menuBar;
        private readonly Map<App, IApp> _apps = new Map<App, IApp>();
        private IApp _currentApp;

        public PadUI(ClickUIBranch parentBranch)
        {
            _branch = new ClickUIBranch("Pad", (int)ClickUIPriorities.Pad);
            parentBranch.Add(_branch);
            _menuBar = new MenuBarUI(_branch);
            _currentApp = new NoneApp();
            World.Subscribe(EventSubscription.Create<AppChanged>(x => OpenApp(x.App), this));
        }

        public void Update(TimeSpan delta)
        {
            _apps.ForEach(x => x.Value.Update(delta));
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            _branch.ParentLocation = absoluteTransform.Location;
            World.Draw("Images/PAD/background", new Transform2(absoluteTransform.Location, new Size2(1600, 900)));
            _menuBar.Draw(absoluteTransform);
            _currentApp.Draw(absoluteTransform + new Transform2(new Vector2(0, 75)));
        }

        public void OpenApp(App app)
        {
            if (_currentApp.Type == app)
                return;
            if (!_apps.ContainsKey(app))
                _apps[app] = MakeApp(app);
            _branch.Remove(_currentApp.Branch);
            _currentApp = _apps[app];
            _branch.Add(_currentApp.Branch);
        }

        private IApp MakeApp(App app)
        {
            if (app.Equals(App.Call))
                return new CallAppUI();
            /*if (app.Equals(App.Food))
                return new FoodApp(_branch);
            if (app.Equals(App.Notification))
                return new NotificationApp(_branch);*/
            throw new KeyNotFoundException($"Unknown App Type {app}");
        }
    }
}
