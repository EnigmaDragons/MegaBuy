using System.Collections.Generic;
using MegaBuy.Shopping.Foods;
using MegaBuy.UIs;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Shopping
{
    public class ShoppingCompanyUI
    {
        public ClickUIBranch Branch { get; }

        private readonly IShoppingCompany _company;
        private readonly Transform2 _transform = Transform2.Zero;
        private readonly List<ShoppingItemUI> _itemsUI = new List<ShoppingItemUI>();

        public ShoppingCompanyUI(IShoppingCompany company)
        {
            Branch = new ClickUIBranch("McKing Jr's", (int)ClickUIPriorities.Pad);
            _company = company;
            for (var i = 0; i < _company.Items.Count; i++)
            {
                var item = _company.Items[i];
                var option = new ShoppingItemUI(item, i, () => _company.Buy(item));
                _itemsUI.Add(option);
                Branch.Add(option.Branch);
            }
        }

        public void Draw(Transform2 parentTransform)
        {
            var absoluteTransform = parentTransform + _transform;
            Branch.ParentLocation = absoluteTransform.Location;
            _itemsUI.ForEach(x => x.Draw(absoluteTransform));
        }
    }
}
