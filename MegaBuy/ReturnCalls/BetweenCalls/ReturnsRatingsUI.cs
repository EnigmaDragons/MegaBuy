using MegaBuy.Calls.Events;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;

namespace MegaBuy.ReturnCalls.BetweenCalls
{
    public class ReturnsRatingsUI : IVisual
    {
        private int _rating;

        public Transform2 Transform { get; }

        public ReturnsRatingsUI()
        {
            Transform = new Transform2(new Size2(Sizes.Star.Width * 3, Sizes.Star.Height));
            World.Subscribe(EventSubscription.Create<CallRated>(OnCallRated, this));
        }

        public void Draw(Transform2 parentTransform)
        {
            for (var i = 0; i < 3; i++)
            {
                var starTransform = parentTransform + new Transform2(new Vector2(i * Sizes.Star.Width, 0), Sizes.Star);
                World.Draw(i < _rating ? "Images/UI/star-filled" : "Images/UI/star-empty", starTransform);
            }
        }

        private void OnCallRated(CallRated rating)
        {
            _rating = rating.RatingScore;
        }
    }
}
