using Microsoft.Xna.Framework;
using MonoDragons.Core.Inputs;

namespace MegaBuy.Player
{
    public class Movement
    {
        private readonly Direction _dir;
        private readonly float _distance;

        public Movement(float distance, Direction dir)
        {
            _dir = dir;
            _distance = distance;
        }

        public Vector2 GetDelta()
        {
            return new Vector2(GetXDirection() * _distance, GetYDirection() * _distance);
        }

        private float GetXDirection()
        {
            switch (_dir.HDir)
            {
                case HorizontalDirection.Left:
                    return -1.0f;
                case HorizontalDirection.Right:
                    return 1.0f;
                default:
                    return 0;
            }
        }

        private float GetYDirection()
        {
            switch (_dir.VDir)
            {
                case VerticalDirection.Down:
                    return 1.0f;
                case VerticalDirection.Up:
                    return -1.0f;
                default:
                    return 0;
            }
        }
    }
}
