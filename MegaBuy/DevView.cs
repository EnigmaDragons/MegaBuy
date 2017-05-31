using System.Collections.Generic;
using MegaBuy.Jobs;
using MegaBuy.MegaBuyCorporation;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy
{
    public class DevView : IVisual
    {
        private readonly Transform2 _transform = Transform2.Zero;
        private readonly List<ImageTextButton> _buttons = new List<ImageTextButton>();

        private bool _visible = false;

        public ClickUIBranch Branch { get; }

        public DevView()
        {
            Branch = new ClickUIBranch("Dev", (int)ClickUIPriorities.Dev);
            var button4 = ImageTextButtonFactory.Create("Return Specialist 1", new Vector2(Sizes.Margin * 4 + Sizes.Button.Width * 3, Sizes.Margin),
                () => Promote(Job.ReturnSpecialistLevel1));
            _buttons.Add(button4);
            Input.On(Control.Select, Toggle);
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!_visible)
                return;
            World.Draw("Images/UI/darkness", Vector2.Zero);
            _buttons.ForEach(x => x.Draw(parentTransform + _transform));
        }

        private void Toggle()
        {
            if (!_visible)
                Show();
            else
                Hide();
        }

        private void Show()
        {
            _visible = true;
            _buttons.ForEach(x => Branch.Add(x));
        }

        private void Hide()
        {
            _visible = false;
            _buttons.ForEach(x => Branch.Remove(x));
        }

        private void Promote(Job role)
        {
            Hide();
            World.Publish(new JobChanged(role));
        }
    }
}
