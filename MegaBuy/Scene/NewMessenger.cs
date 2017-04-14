using System;
using MegaBuy.Temp.UIThings;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Scene
{
    public class NewMessenger : IScene
    {
        private CallMessengerUI _messenger;

        public void Init()
        {
            _messenger = new CallMessengerUI();
            _messenger.AddMessage("hello", false);
            _messenger.AddMessage("how are you?", false);
            _messenger.AddMessage("Horrible, go away!", true);
            _messenger.AddMessage("you are being ridiculious i shall have a donut?", false);
            _messenger.AddMessage("and coffee", false);
            _messenger.AddMessage("and a party hat just for fun?", false);
            _messenger.AddMessage("thats my hat! How could you?", true);
            _messenger.AddMessage("because i'm the dark destroyer of all worlds. I live to make people like you depressed you foolish insect!", false);
            _messenger.AddMessage("you fool", false);
            _messenger.AddMessage("i have so much more to say", false);
            _messenger.AddMessage("you fool", false);
            _messenger.AddMessage("you fool", false);
            _messenger.AddMessage("you fool", false);
            _messenger.AddMessage("you fool", false);
        }

        public void Update(TimeSpan delta)
        {
            _messenger.Update(delta);
        }

        public void Draw()
        {
            _messenger.Draw(Transform2.Zero);
        }
    }
}
