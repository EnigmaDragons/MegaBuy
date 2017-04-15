using System;
using MegaBuy.Policies;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.UserInterface;
using Microsoft.Xna.Framework;
using MegaBuy.Calls;
using MegaBuy.Calls.Events;
using MegaBuy.Money;
using MegaBuy.Money.Accounts;
using MegaBuy.Money.Amounts;

namespace MegaBuy.Scene
{
    public class TimTest : IScene
    {
        private readonly ClickUI UI = new ClickUI();
        private string log = "start";

        public void Init()
        {
            var branch = new ClickUIBranch("asd", 2);
            UI.Add(branch);
            var subBranch = new ClickUIBranch("asdf", 4);
            branch.Add(subBranch);
            var branch2 = new ClickUIBranch("bob", 3);
            UI.Add(branch2);
            branch.Add(new SimpleClickable( new Rectangle(new Point(0, 0), new Point(100, 100)), () => Test(branch)));
            subBranch.Add(new SimpleClickable( new Rectangle(new Point(0, 0), new Point(100, 100)), () => Test(subBranch)));
            branch2.Add(new SimpleClickable( new Rectangle(new Point(0, 0), new Point(100, 100)), () => Test(branch2)));

            branch.Add(new SimpleClickable( new Rectangle(new Point(100, 0), new Point(100, 100)), () => Test(branch)));
            subBranch.Add(new SimpleClickable( new Rectangle(new Point(100, 0), new Point(100, 100)), () => Test(subBranch)));

            subBranch.Add(new SimpleClickable( new Rectangle(new Point(200, 0), new Point(100, 100)), () => Test(subBranch)));
            branch2.Add(new SimpleClickable( new Rectangle(new Point(200, 0), new Point(100, 100)), () => Test(branch2)));

            branch.Add(new SimpleClickable( new Rectangle(new Point(300, 0), new Point(100, 100)), () => Test(branch)));
            branch2.Add(new SimpleClickable( new Rectangle(new Point(300, 0), new Point(100, 100)), () => Test(branch2)));

            var b1 = new ClickUIBranch("b", 5) { Location = new Vector2(100, 100) };
            UI.Add(b1);
            var sb = new ClickUIBranch("sb", 6) { Location = new Vector2(100, 100) };
            b1.Add(sb);
            var ssb = new ClickUIBranch("ssb", 7) { Location = new Vector2(100, 100) };
            sb.Add(ssb);
            if (b1.Location.X != sb.ParentLocation.X || b1.Location.X + 100 != ssb.ParentLocation.X)
                throw new Exception();
            sb.Location = new Vector2(200, 200);
            if (b1.Location.X + 200 != ssb.ParentLocation.X)
                throw new Exception();
            b1.Location = new Vector2(200, 200);
            if (b1.Location.X + 200 != ssb.ParentLocation.X || b1.Location.X != 200)
                throw new Exception();
            var x = new SimpleClickable( new Rectangle(new Point(0, 0), new Point(100, 100)), () => Test(ssb));
            ssb.Add(x);
            if (ssb.GetElement(new Point(501, 501)) != x)
                throw new Exception();

            new MegaBuyAccounting(new PlayerAccount());
            World.Publish(new TechnicalMistakeOccurred(new Fee(5), new Policy("asd")));
        }

        public void Test(object obj)
        {
            log += " " + obj.ToString();
        }

        public void Update(TimeSpan delta)
        {
            UI.Update(delta);
        }

        public void Draw()
        {
        }
    }
}
