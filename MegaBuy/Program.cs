using System;
using System.Collections.Generic;
using MegaBuy.Endings;
using MegaBuy.Scene;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.EngimaDragons;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Navigation;
using MonoDragons.Core.Render;

namespace MegaBuy
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new NeedlesslyComplexMainGame("MegaBuy", "InGame", new Display(1600, 900, false), CreateSceneFactory(), CreateKeyboardController()))
                game.Run();
        }

        private static IController CreateKeyboardController()
        {
            return new KeyboardController(new Map<Keys, Control>
            {
                { Keys.OemTilde, Control.Select },
                { Keys.Enter, Control.Start },
                { Keys.V, Control.A },
                { Keys.O, Control.X }
            });
        }

        private static SceneFactory CreateSceneFactory()
        {
            return new SceneFactory(new Dictionary<string, Func<IScene>>
            {
                { "Logo", () => new FadingInScene(new LogoScene()) },
                { "MainMenu", () => new Menu() },
                { "CharacterCreation", () => new CharacterCreation() },
                { "InGame", () => new InGame() },
                { "Starved", () => new FadingInScene(new Starved()) },
                { "Evicted", () => new FadingInScene(new Evicted()) },
                { "Fired", () => new FadingInScene(new Fired()) },
                { "HeartAttack", () => new FadingInScene(new HeartAttack()) },
                { "Graph", () => new Graph() },
            });
        }
    }
#endif
}
