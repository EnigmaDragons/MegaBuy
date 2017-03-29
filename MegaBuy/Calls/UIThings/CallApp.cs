using System;
using System.Collections.Generic;
using MegaBuy.CustomUI;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Calls.UIThings
{
    public class CallApp : IVisualAutomaton
    {
        //1400
        //900
        private Size2 caller = new Size2(400, 450);
        private Size2 messengertext = new Size2(400, 450);
        private ClickUI _ui;
        private Call _call;
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private Timer _timer;
        private readonly TextMessenger _messenger = new TextMessenger(6, Color.White, new Size2(350, 50));
        private int _index = 0;

        public CallApp(Call call)
        {
            _call = call;
            _ui = new ClickUI();
            for (var i = 0; i < call.Options.Count; i++)
            {
                var button = new TextButton(1, new Rectangle(((int)(i / 2)) * 350, ((i % 2) * 150) + 450, 300, 100), call.Options[i].Go, call.Options[i].Description, Color.FromNonPremultiplied(42, 42, 42, 250), Color.FromNonPremultiplied(30, 30, 30, 250), Color.FromNonPremultiplied(21, 21, 21, 250));
                _ui.Add(button);
                _visuals.Add(button);
            }
            _timer = new Timer(AddMessage, 1000);
        }

        public void Update(TimeSpan delta)
        {
            _ui.Update(delta);
            _timer.Update(delta);
        }

        public void Draw(Transform2 parentTransform)
        {
            _ui.Position = parentTransform.Location;
            World.Draw(new RectangleTexture(1400, 900, Color.Red).Create(), parentTransform.Location);
            var callerTransform = new Transform2(parentTransform.Location, caller);
            World.Draw(new RectangleTexture(caller.Width, caller.Height, Color.Gray).Create(), callerTransform);
            World.Draw("Images/Screen/female-customer", callerTransform);
            World.Draw(new RectangleTexture(messengertext.Width, messengertext.Height, Color.Gray).Create(), new Transform2(new Vector2(parentTransform.Location.X + 900, parentTransform.Location.Y + 0), messengertext));
            _visuals.ForEach(x => x.Draw(parentTransform));
            _messenger.Draw(new Transform2(new Vector2(parentTransform.Location.X + 925, 0)));
        }

        public void AddMessage()
        {
            if (_index == _call.Script.Count)
                return;
            _messenger.AddMessage(_call.Script[_index].Text, _call.Script[_index].CharacterName == "player" ? Color.LightBlue : Color.LightGreen);
            _index++;
        }
    }
}
