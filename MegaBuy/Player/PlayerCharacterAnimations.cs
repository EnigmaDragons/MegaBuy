using MonoDragons.Core.Engine;
using MonoDragons.Core.Render;

namespace MegaBuy.Player
{
    public sealed class PlayerCharacterAnimations : Animations
    {
        public PlayerCharacterAnimations()
            : base(Get(), "Up-False") { }

        private static Map<string, Animation> Get()
        {
            var downFalse = new Animation(180, "Images/Character/d1");
            var downTrue = new Animation(180, "Images/Character/d1");

            var leftFalse = new Animation(180, "Images/Character/l1");
            var leftTrue = new Animation(180, "Images/Character/l1");

            var rightFalse = new Animation(180, "Images/Character/r1");
            var rightTrue = new Animation(180, "Images/Character/r1");

            var upFalse = new Animation(180, "Images/Character/u1");
            var upTrue = new Animation(180, "Images/Character/u1");

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
