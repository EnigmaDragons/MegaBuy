using System;
using System.Collections.Generic;
using MegaBuy.MegaBuyCorporation.Policies;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Policies
{
    // @todo #TEST Make Policy Pages look nice
    public sealed class PolicyPageUI : IVisual
    {
        private readonly ColoredRectangle _pageRect;
        private readonly ActivePolicies _policies;
        private readonly int _pagePolicyIndex;
        private readonly int _pagePolicyCount;

        private readonly List<string> _policyTexts = new List<string>();


        public bool CanTravelBack => _pagePolicyIndex > 0;
        public bool CanTravelNext => _pagePolicyIndex + _pagePolicyCount < _policies.Count;

        public PolicyPageUI(ActivePolicies policies)
            : this (policies, 0, 10) { }

        public PolicyPageUI(ActivePolicies policies, int pagePolicyIndex, int pagePolicyCount)
        {
            _policies = policies;
            _pagePolicyIndex = pagePolicyIndex;
            _pagePolicyCount = pagePolicyCount;
            _pageRect = new ColoredRectangle {  Color = Color.Blue, Transform = new Transform2(new Vector2(0, 20), new Size2(1600, 600)) };
            UpdatePolicyTexts();
        }

        private void UpdatePolicyTexts()
        {
            _policyTexts.Clear();
            _policyTexts.AddRange(_policies.GetPolicyTexts(_pagePolicyIndex, _pagePolicyCount));
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
            _pageRect.Draw(parentTransform);
            for (var i = 0; i < _policyTexts.Count; i++)
                UI.DrawText(_policyTexts[i], new Vector2(50, 40 + (i * 50)) + parentTransform.Location, Color.White);
        }
    }
}
