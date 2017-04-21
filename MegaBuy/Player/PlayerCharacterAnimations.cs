using MonoDragons.Core.Engine;
using MonoDragons.Core.Render;

namespace MegaBuy.Player
{
    public sealed class PlayerCharacterAnimations : Animations
    {
        private static string path = "Images/Character/";

        public PlayerCharacterAnimations(CharacterSex sex)
            : base(Get(sex), "Up-False") { }

        private static Map<string, Animation> Get(CharacterSex sex)
        {
            var sexChar = sex.ToString().ToLower()[0];
            var pre = path + sexChar;

            var downFalse = new Animation(180, $"{pre}d1");
            var downTrue = new Animation(180, $"{pre}d1", $"{pre}d2", $"{pre}d1", $"{pre}d3");

            var leftFalse = new Animation(180, $"{pre}l1");
            var leftTrue = new Animation(180, $"{pre}l1", $"{pre}l2", $"{pre}l1", $"{pre}l3");

            var rightFalse = new Animation(180, $"{pre}r1");
            var rightTrue = new Animation(180, $"{pre}r1", $"{pre}r2", $"{pre}r1", $"{pre}r3");

            var upFalse = new Animation(180, $"{pre}u1");
            var upTrue = new Animation(180, $"{pre}u1", $"{pre}u2", $"{pre}u1", $"{pre}u3");

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
