using System;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Calls;
using MegaBuy.Calls.UI;
using MegaBuy.CustomUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        
        private readonly ClickUILayer _layer;
        private Call _call;
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private Timer _timer;
        private AutoSizingTextMessenger _messenger;
        private int _index;
        private string _person;

        private const int _stars = 5;


        private readonly Texture2D _backgroundRect = new RectangleTexture(new Size2(1380, 880), Color.FromNonPremultiplied(0, 0, 0, 50)).Create();
        private readonly Texture2D _messengerRect = new RectangleTexture(new Size2(900, 600), Color.FromNonPremultiplied(0, 0, 0, 100)).Create();
        private readonly Texture2D _callerRect = new RectangleTexture(new Size2(400, 570), Color.FromNonPremultiplied(0, 0, 0, 100)).Create();


        public CallApp(ClickUI ui)
        {
            _layer = new ClickUILayer();
            ui.Add(_layer);
            _timer = new Timer(AddMessage, 1000);
            World.Subscribe(new EventSubscription<CallSucceeded>(x => CallEnded(), this));
            World.Subscribe(new EventSubscription<CallFailed>(x => CallEnded(), this));
            World.Subscribe(new EventSubscription<CallStarted>(x => UpdateCall(x.Call), this));
            CallEnded();
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

        private void CallEnded()
        {
            _person = "nothing";
            _visuals.Clear();
            _layer.Clear();
            _index = 0;
            _call = null;
            _messenger = new AutoSizingTextMessenger(6, Color.Black);
            DisplayStars(_stars);
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
                _visuals.Add(i <= _stars ? (IVisual)new WholeStar(new Vector2(150 * i + 180, 230)) : (IVisual)new EmptyStar(new Vector2(150 * i + 180, 230)));
        }

        private void PublishReadyForCall()
        {
            _layer.Clear();
            _visuals.Clear();
            World.Publish(new AgentCallStatusChanged(AgentCallStatus.Available));
        }

        public void Update(TimeSpan delta)
        {
            _timer.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            _layer.Location = parentTransform.Location;

            World.Draw(_backgroundRect, new Vector2(210, 10));

            World.Draw(_callerRect, new Vector2(1160, 60));
            World.Draw("Images/Customers/" + _person.ToLower().Replace(' ', '-'), new Rectangle(1160, 60, 400, 580));
            UI.DrawText(_person, new Vector2(1160, 20), Color.Green, "Fonts/Audiowide");

            World.Draw(_messengerRect, new Vector2(230, 30));
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
