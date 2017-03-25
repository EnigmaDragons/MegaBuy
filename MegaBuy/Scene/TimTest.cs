using MegaBuy.Calls;
using MegaBuy.Calls.Options;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaBuy.Scene
{
    public class TimTest : IScene
    {
        private CallerScript _script;
        private ClickUI _clickUi;
        private Random _rng = new Random();

        public void Init()
        {
            _clickUi = new ClickUI();
            World.Subscribe<CallSucceeded>(new EventSubscription<CallSucceeded>((c) => this.CallEnded(), this));
            World.Subscribe<CallFailed>(new EventSubscription<CallFailed>((c) => this.CallEnded(), this));
            _script = new CallerScript(5000, "My flying car just arrived, but I can't get it to turn on!", _clickUi, new List<OptionReaction> {
                new OptionReaction(new ReferToTroubleShooting(), (a) => true , (a) => true, "Correct"),
                new OptionReaction(new ReferToInfo(), (a) => true, (a) => false, incorrectMessage: "Incorrect") });
        }

        public void Draw()
        {
            World.Draw("Images/Screen/screen2", new Rectangle(0, 0, 1600, 900));
            UI.DrawCenteredWithOffset("Images/Screen/male-customer", new Vector2(0, -20));
            World.Draw("Images/Screen/conversation", new Vector2(0, 700));
            _script.Draw(Transform2.Zero);
        }

        public void CallEnded()
        {
            _script.Dispose();
            if (_rng.Next(2) > 0)
                _script = new CallerScript(5000, "Do you just have insurance policies for flying cars bought from this company or in general?",
                    _clickUi, new List<OptionReaction> {
                        new OptionReaction(new ReferToTroubleShooting(), (a) => true , (a) => false, incorrectMessage: "Incorrect"),
                        new OptionReaction(new ReferToInfo(), (a) => true, (a) => true, "Correct") });
            else
                _script = new CallerScript(5000, "sjdofisorheworihewroinnoiweroijijowirjeoisldkfjlcxlk?", _clickUi,
                    new List<OptionReaction> {
                        new OptionReaction(new TranslateButton(), (a) => true, (a) => true, "My computer has a virus, what should I do?"),
                        new OptionReaction(new ReferToTroubleShooting(), (a) => true , (a) => a.ContainsTypeOf(typeof(TranslateButton))),
                        new OptionReaction(new ReferToInfo(), (a) => true, (a) => false) });
        }

        public void Update(TimeSpan delta)
        {
            _clickUi.Update(delta);
            _script.Update(delta);
        }
    }
}
