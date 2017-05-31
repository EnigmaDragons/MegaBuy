using System;
using System.Collections.Generic;
using MegaBuy.Pads.Apps;
using MegaBuy.Shopping.Foods;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Audio;

namespace MegaBuy.Shopping
{
    public class ShoppingApp : IApp
    {
        private readonly List<IShoppingCompany> _companies = new List<IShoppingCompany>
        {
            new McKingJrs(),
            new SweetBakery(),
        };

        private readonly Transform2 _transform = new Transform2(new Vector2(350, Sizes.Margin));
        private readonly List<ShoppingCompanyOptionUI> _companyOptionUIs = new List<ShoppingCompanyOptionUI>();
        private readonly ClickUIBranch _companiesBranch;
        private readonly ImageTextButton _return;

        private bool _isSelectingCompany = true;
        private ShoppingCompanyUI _currentCompany;

        public App Type => App.Shopping;
        public ClickUIBranch Branch { get; }

        public ShoppingApp()
        {
            World.Subscribe(EventSubscription.Create<FoodDelivered>((f) => NewMethod(), this));
            Branch = new ClickUIBranch("Item App", (int)ClickUIPriorities.Pad);
            _companiesBranch = new ClickUIBranch("Companies", (int)ClickUIPriorities.Pad);
            Branch.Add(_companiesBranch);

            for (var i = 0; i < _companies.Count; i++)
            {
                var company = _companies[i];
                var companyUI = new ShoppingCompanyUI(company);
                var option = new ShoppingCompanyOptionUI(company, i, () => NavigateToCompany(companyUI));
                _companyOptionUIs.Add(option);
                _companiesBranch.Add(option.Branch);
            }
            _return = ImageTextButtonFactory.Create("Return", new Vector2(1000, 625), NavigateToCompanySelection);
        }

        private void NewMethod()
        {
            Audio.PlaySound("Notification");
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            Branch.ParentLocation = absoluteTransform.Location;
            if (_isSelectingCompany)
                _companyOptionUIs.ForEach(x => x.Draw(absoluteTransform));
            else
            {
                _currentCompany.Draw(absoluteTransform);
                _return.Draw(absoluteTransform);
            }
        }

        private void NavigateToCompany(ShoppingCompanyUI company)
        {
            _currentCompany = company;
            Branch.Remove(_companiesBranch);
            Branch.Add(_currentCompany.Branch);
            Branch.Add(_return);
            _isSelectingCompany = false;
        }

        private void NavigateToCompanySelection()
        {
            Branch.Remove(_currentCompany.Branch);
            Branch.Add(_companiesBranch);
            Branch.Remove(_return);
            _isSelectingCompany = true;
        }
    }
}
