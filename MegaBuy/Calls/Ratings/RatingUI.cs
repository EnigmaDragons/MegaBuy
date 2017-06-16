using MegaBuy.Calls.Events;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.Calls.Ratings
{
    public class RatingUI : IVisual
    {
        private readonly Transform2 _transform = new Transform2(new Vector2(400, Sizes.Margin));
        private int _rating;

        public RatingUI()
        {
            World.Subscribe(EventSubscription.Create<CallRated>(RateCall, this));
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            for (var i = 0; i < 5; i++)
            {
                var starTransform = absoluteTransform + new Transform2(new Vector2(i * (Sizes.Star.Width + Sizes.Margin), 0), Sizes.Star);
                World.Draw(i < _rating ? "Images/UI/star-filled" : "Images/UI/star-empty", starTransform);
            }
        }

        private void RateCall(CallRated rating)
        {
            _rating = rating.RatingScore;
        }
    }
}
