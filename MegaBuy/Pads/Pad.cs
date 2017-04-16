using System;
using System.Collections.Generic;
using MegaBuy.Calls;
using MegaBuy.Pads.Apps;
using MegaBuy.Temp;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Pads
{
    public class Pad : IVisualAutomaton
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(0, 0));
        private readonly ClickUIBranch _branch;
        private readonly MenuBar _menuBar;
        private readonly Map<App, IApp> _apps = new Map<App, IApp>();
        private IApp _currentApp;

        public Pad(ClickUIBranch parentBranch)
        {
            _branch = new ClickUIBranch("Pad", (int)ClickUIPriorities.Pad);
            parentBranch.Add(_branch);
            _menuBar = new MenuBar(_branch);
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
            _currentApp.Draw(absoluteTransform + new Transform2(new Vector2(0, 125)));
            World.Draw("Images/UI/line-vertical", absoluteTransform + new Transform2(new Vector2(0, 757), Sizes.VerticalLine));
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
                return new CallApp();
            /*if (app.Equals(App.Food))
                return new FoodApp(Branch);
            if (app.Equals(App.Notification))
                return new NotificationApp(Branch);*/
            throw new KeyNotFoundException($"Unknown App Type {app}");
        }
    }
}
