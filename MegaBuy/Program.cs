using System;
using System.Collections.Generic;
using MegaBuy.Calls;
using MegaBuy.Endings;
using MegaBuy.Scene;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.EngimaDragons;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Navigation;

namespace MegaBuy
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MainGame("PAD", new ScreenSettings(1600, 900, false), CreateSceneFactory(), CreateKeyboardController()))
                game.Run();
        }

        private static IController CreateKeyboardController()
        {
            return new KeyboardController(new Map<Keys, Control>
            {
                { Keys.Enter, Control.Start },
                { Keys.V, Control.A }
            });
        }

        private static SceneFactory CreateSceneFactory()
        {
            return new SceneFactory(new Dictionary<string, Func<IScene>>
            {
                { "Logo", () => new FadingInScene(new LogoScene()) },
                { "Darkness", () => new Darkness() },
                { "Room", () => new Room() },
                { "TickTock", () => new TickTock() },
                { "ILovePolitics", () => new ILovePolitics() },
                { "Screen", () => new Screen() },
                { "ClickToWin", () => new ClickToWin() },
                { "MainMenu", () => new Menu() },
                { "Starved", () => new Starved() },
                { "SlowlyStarving", () => new SlowlyStarving() },
                { "PAD", () => new PAD() },
                { "Texting", () => new TextMessangerSample() },
            });
        }
    }
#endif
}
