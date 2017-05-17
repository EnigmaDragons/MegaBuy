using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using MegaBuy.Calls.Rules;
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
            var button1 = ImageTextButtonFactory.Create("Referer 1", new Vector2(Sizes.Margin, Sizes.Margin),
                () => Promote(JobRole.ReferrerLevel1));
            _buttons.Add(button1);
            var button2 = ImageTextButtonFactory.Create("Referer 2", new Vector2(Sizes.Margin*2 + Sizes.Button.Width, Sizes.Margin),
                () => Promote(JobRole.ReferrerLevel2));
            _buttons.Add(button2);
            var button3 = ImageTextButtonFactory.Create("Referer 3", new Vector2(Sizes.Margin*3 + Sizes.Button.Width*2, Sizes.Margin),
                () => Promote(JobRole.ReferrerLevel3));
            _buttons.Add(button3);
            var button4 = ImageTextButtonFactory.Create("Return Specialist 1", new Vector2(Sizes.Margin * 4 + Sizes.Button.Width * 3, Sizes.Margin),
                () => Promote(JobRole.ReturnSpecialistLevel1));
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

        private void Promote(JobRole role)
        {
            Hide();
            World.Publish(new JobRoleAccepted(role));
        }
    }
}
