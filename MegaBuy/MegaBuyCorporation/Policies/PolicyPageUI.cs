using System;
using System.Collections.Generic;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.MegaBuyCorporation.Policies
{
    public sealed class PolicyPageUI : IVisual
    {
        private readonly int _pagePolicyIndex;
        private readonly int _pagePolicyCount;

        private readonly List<IVisual> _policyTexts = new List<IVisual>();

        private ActivePolicies _policies;

        public bool CanTravelBack => _pagePolicyIndex > 0;
        public bool CanTravelNext => _pagePolicyIndex + _pagePolicyCount < _policies.Count;

        public PolicyPageUI(ActivePolicies policies)
            : this (policies, 0, 10) { }

        public PolicyPageUI(ActivePolicies policies, int pagePolicyIndex, int pagePolicyCount)
        {
            _policies = policies;
            _pagePolicyIndex = pagePolicyIndex;
            _pagePolicyCount = pagePolicyCount;
            UpdatePolicies();
            World.Subscribe(EventSubscription.Create<PolicyChanged>(x => UpdatePolicies(), this));
        }

        private void UpdatePolicies()
        {
            _policyTexts.Clear();
            _policies.GetPolicyTexts(_pagePolicyIndex, _pagePolicyCount).ForEach(x => _policyTexts.Add(new ImageLabel(x, "Images/UI/Policy", new Transform2(new Vector2(Sizes.Margin * 2 + Sizes.SideButton.Width, Sizes.Margin), Sizes.Policy))));
        }

        public PolicyPageUI GetNextPage()
        {
            return new PolicyPageUI(_policies, _pagePolicyIndex + _pagePolicyCount, _pagePolicyCount);
        }

        public PolicyPageUI GetPreviousPage()
        {
            return new PolicyPageUI(_policies, Math.Max(0, _pagePolicyIndex - _pagePolicyCount), _pagePolicyCount);
        }

        public void Draw(Transform2 parentTransform)
        {
            for (var i = 0; i < _policyTexts.Count; i++)
                _policyTexts[i].Draw(new Transform2(new Vector2(0, i * (Sizes.Policy.Height + Sizes.SmallMargin))) + parentTransform.Location);
        }
    }
}
