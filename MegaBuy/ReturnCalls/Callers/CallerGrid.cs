using System.Collections.Generic;
using MegaBuy.Calls.Callers;
using MegaBuy.ReturnCalls.Choices;
using MegaBuy.ReturnCalls.PurchaseHistories;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.UserInterface.Layouts;

namespace MegaBuy.ReturnCalls.Callers
{
    public class CallerGrid : IVisualControl
    {
        private List<IVisual> _visuals = new List<IVisual>();
        private List<ClickUIBranch> _branches = new List<ClickUIBranch>();

        private bool _isViewing = true;

        public ClickUIBranch Branch { get; }

        public CallerGrid(Size2 size)
        {
            Branch = new ClickUIBranch("Caller Grid", (int)ClickUIPriorities.Pad);

            var callerGrid = new GridLayout(size,
                new List<Definition> { new ShareDefintion(), new ConcreteDefinition(300) },
                new List<Definition>
                {
                    new ConcreteDefinition(150),
                    new ConcreteDefinition(150),
                    new ConcreteDefinition(150),
                    new ShareDefintion()
                });

            var caller = new CallerFaceUI();
            var ready = new ReturnsCallReadyUI();
            var callerInfoTransform = new Transform2(callerGrid.GetBlockSize(1, 4, 2, 1) - new Size2(50, 0));
            var callerInfo = new ReturnsCallerInfo(callerInfoTransform);
            var callChoicesTransform = new Transform2(callerGrid.GetBlockSize(1, 1, 1, 2));
            var callChoices = new ChoicesUI(callChoicesTransform);
            var purchaseHistoryNav = new PurchaseHistoryNavigationUI();

            callerGrid.AddSpatial(caller, caller.Transform, 2, 1, 1, 3);
            callerGrid.AddSpatial(ready, ready.Transform, 1, 1, 1, 3);
            callerGrid.AddSpatial(purchaseHistoryNav, purchaseHistoryNav.Transform, 1, 3);
            callerGrid.AddSpatial(callerInfo, callerInfoTransform, 1, 4, 2, 1);
            callerGrid.AddSpatial(callChoices, callChoicesTransform, 1, 1, 1, 2);

            _branches.Add(ready.Branch);
            _branches.Add(callChoices.Branch);
            _branches.Add(purchaseHistoryNav.Branch);
            Branch.Add(ready.Branch);
            Branch.Add(callChoices.Branch);
            Branch.Add(purchaseHistoryNav.Branch);
            _visuals.Add(callerGrid);

            World.Subscribe(EventSubscription.Create<PurchaseHistoryViewed>(x => OnPurchaseHistoryViewed(), this));
            World.Subscribe(EventSubscription.Create<CallerViewed>(x => OnCallerViewed(), this));
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_isViewing)
                _visuals.ForEach(x => x.Draw(parentTransform));
        }

        private void OnPurchaseHistoryViewed()
        {
            _branches.ForEach(x => Branch.Remove(x));
            _isViewing = false;
        }

        private void OnCallerViewed()
        {
            _branches.ForEach(x => Branch.Add(x));
            _isViewing = true;
        }
    }
}
