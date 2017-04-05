﻿using System;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Calls;
using MegaBuy.Calls.UI;
using MegaBuy.CustomUI;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Apps
{
    public class CallApp : IApp
    {
        public App Type => App.Call;

        private readonly ClickUI _ui;
        private readonly ClickUILayer _layer;
        private Call _call;
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private Timer _timer;
        private AutoSizingTextMessenger _messenger;
        private int _index;
        private string _person;

        public CallApp(ClickUI ui)
        {
            _ui = ui;
            _layer = new ClickUILayer();
            _ui.Add(_layer);
            _timer = new Timer(AddMessage, 1000);
            World.Subscribe(new EventSubscription<CallSucceeded>(x => CallEnded(x.Rating.AsInt()), this));
            World.Subscribe(new EventSubscription<CallFailed>(x => CallEnded(0), this));
            World.Subscribe(new EventSubscription<CallStarted>(x => UpdateCall(x.Call), this));
            CallEnded(4);
        }

        private void UpdateCall(Call call)
        {
            _call = call;
            _person = _call.Script.First(x => x.CharacterName != "player").CharacterName;
            for (var i = 0; i < call.Options.Count; i++)
            {
                var button = new TextButton(1, new Rectangle(i * 400 + 100, 720, 300, 90), call.Options[i].Go, call.Options[i].Description, Color.FromNonPremultiplied(42, 42, 42, 250), Color.FromNonPremultiplied(30, 30, 30, 250), Color.FromNonPremultiplied(21, 21, 21, 250));
                _layer.Add(button);
                _visuals.Add(button);
            }
        }

        private void CallEnded(int x)
        {
            _person = "nothing";
            _visuals.Clear();
            _layer.Clear();
            _index = 0;
            _call = null;
            _messenger = new AutoSizingTextMessenger(6, Color.Black);
            DisplayStars(x);
            var button = new TextButton(1, new Rectangle(550, 720, 300, 90),
                PublishReadyForCall,
                "Ready",
                Color.FromNonPremultiplied(42, 42, 42, 250), 
                Color.FromNonPremultiplied(30, 30, 30, 250),
                Color.FromNonPremultiplied(21, 21, 21, 250));
            _visuals.Add(button);
            _layer.Add(button);
        }

        private void DisplayStars(int stars)
        {
            for (var i = 1; i < 6; i++)
                _visuals.Add(i <= stars ? (IVisual)new WholeStar(new Vector2(150 * i + 180, 230)) : (IVisual)new EmptyStar(new Vector2(150 * i + 180, 230)));
        }

        private void PublishReadyForCall()
        {
            _layer.Clear();
            _visuals.Clear();
            World.Publish(new AgentCallStatusChanged(AgentCallStatus.Available));
        }

        public void Update(TimeSpan delta)
        {
            if (_call != null)
                _call.Update(delta);
            _ui.Update(delta);
            _timer.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            _layer.Location = parentTransform.Location;

            //World.Draw(new RectangleTexture(new Size2(1380, 880), Color.FromNonPremultiplied(0, 0, 0, 50)).Create(), new Vector2(210, 10));

            World.Draw(new RectangleTexture(new Size2(400, 570), Color.FromNonPremultiplied(0, 0, 0, 100)).Create(), new Vector2(1160, 60));
            World.Draw("Images/Customers/" + _person.ToLower().Replace(' ', '-'), new Rectangle(1160, 60, 400, 580));
            UI.DrawText(_person, new Vector2(1160, 20), Color.Green, "Fonts/Audiowide");

            //World.Draw(new RectangleTexture(new Size2(900, 600), Color.FromNonPremultiplied(0, 0, 0, 100)).Create(), new Vector2(230, 30));
            _messenger.Draw(new Transform2(new Vector2(250, 50)));

            _visuals.ForEach(x => x.Draw(parentTransform));
        }

        public void AddMessage()
        {
            if (_call == null || _index == _call.Script.Count)
                return;
            _messenger.AddMessage(_call.Script[_index].Text, _call.Script[_index].CharacterName.Equals("player", StringComparison.InvariantCultureIgnoreCase) ? Color.FromNonPremultiplied(250, 100, 250, 200) : Color.FromNonPremultiplied(100, 250, 100, 200));
            _index++;
        }
    }
}
