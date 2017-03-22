using System;
using MegaBuy.Policies;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;

namespace MegaBuy.Scene
{
    public sealed class ILovePolitics : IScene
    {
        private readonly MutableTextPage _textPage = new MutableTextPage();
        private readonly ActivePolicies _policies = new ActivePolicies();
        private bool _showingPolicies;

        public void Init()
        {
            Input.On(Control.Start, ToggleShowPolicies);
            _policies.Add(new Policy("1. Transfer all calls"));
            _policies.Add(new Policy("2. Hang up on all callers"));
        }

        public void Update(TimeSpan delta)
        {
            _textPage.Set(_policies.GetPolicyTexts());
        }

        public void Draw()
        {
            if (_showingPolicies)
                _textPage.Draw(Transform.Zero);
        }

        private void ToggleShowPolicies()
        {
            _showingPolicies = !_showingPolicies;
        }
    }
}
