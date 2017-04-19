using MonoDragons.Core.Engine;
using MonoDragons.Core.Render;

namespace MegaBuy.Player
{
    public sealed class PlayerCharacterAnimations : Animations
    {
        private static string path = "Images/Character/";

        public PlayerCharacterAnimations()
            : base(Get(), "Up-False") { }

        private static Map<string, Animation> Get()
        {
            var downFalse = new Animation(180, $"{path}fd1");
            var downTrue = new Animation(180, $"{path}fd1", $"{path}fd2", $"{path}fd3");

            var leftFalse = new Animation(180, $"{path}fl1");
            var leftTrue = new Animation(180, $"{path}fl1", $"{path}fl2", $"{path}fl3");

            var rightFalse = new Animation(180, $"{path}fr1");
            var rightTrue = new Animation(180, $"{path}fr1", $"{path}fr2", $"{path}fr3");

            var upFalse = new Animation(180, $"{path}fu1");
            var upTrue = new Animation(180, $"{path}fu1", $"{path}fu2", $"{path}fu3");

            var animStates = new Map<string, Animation>
            {
                { "Down-False", downFalse },
                { "Down-True", downTrue },
                { "Left-False", leftFalse },
                { "Left-True", leftTrue },
                { "Right-False", rightFalse },
                { "Right-True", rightTrue },
                { "Up-False", upFalse },
                { "Up-True", upTrue },
            };

            return animStates;
        }
    }
}
