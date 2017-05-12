using System;
using MegaBuy.Pads.Apps;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Policies
{
    public sealed class PoliciesApp : IApp
    {
        private readonly Transform2 _transform = Transform2.Zero;

        private readonly ImageTextButton _backButton;
        private readonly ImageTextButton _nextButton;
        private PolicyPageUI _pageUi;

        public ClickUIBranch Branch { get; }
        public App Type => App.Policies;

        public PoliciesApp()
        {
            Branch = new ClickUIBranch("Policies App", (int)ClickUIPriorities.Pad);
            _pageUi = new PolicyPageUI(GameState.ActivePolicies, 0, 7);
            _backButton = ImageTextButtonFactory.CreateRotated("V", new Vector2(Sizes.Margin, 300), NavigateBack, Rotation2.Left);
            _nextButton = ImageTextButtonFactory.CreateRotated("V", new Vector2(1600 - Sizes.Margin, 300), NavigateForward, Rotation2.Right);
            UpdateNavButtons();
        }

        private void NavigateForward()
        {
            _pageUi = _pageUi.GetNextPage();
            UpdateNavButtons();
        }

        private void NavigateBack()
        {
            _pageUi = _pageUi.GetPreviousPage();
            UpdateNavButtons();
        }

        private void UpdateNavButtons()
        {
            if (!_pageUi.CanTravelBack)
                Branch.Remove(_backButton);
            else
                Branch.Add(_backButton);
            if (!_pageUi.CanTravelNext)
                Branch.Remove(_nextButton);
            else
                Branch.Add(_nextButton);
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
            var t = parentTransform + _transform;
            Branch.ParentLocation = t.Location;
            _pageUi.Draw(t);
            if (_pageUi.CanTravelBack)
                _backButton.Draw(t);
            if (_pageUi.CanTravelNext)
                _nextButton.Draw(t);
        }
    }
}
