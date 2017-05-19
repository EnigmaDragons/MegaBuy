using System;
using System.Collections.Generic;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.PurchaseHistories
{
    public class SimplePurchaseHistory : IVIsualAutomatonControl
    {
        public ClickUIBranch Branch { get; }

        public SimplePurchaseHistory(IEnumerable<Purchase> purchases) 
        {
            
        }

        public void Update(TimeSpan delta)
        {

        }

        public void Draw(Transform2 parentTransform)
        {
        }
    }
}
