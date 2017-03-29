using System;
using MonoDragons.Core.Engine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.PhysicsEngine;
using Microsoft.Xna.Framework;

namespace MegaBuy.Scene
{
    public sealed class PAD : IScene
    {
        private ImageButton _btnAvailableForCall;
        private ImageButton _btnOrderFood;
        private ImageButton _btnLogout;
        private ClickUI _ui;

        public void Init()
        {
            _btnAvailableForCall = new ImageButton(
                "Images/PAD/available-default",
                "Images/PAD/available-hover",
                "Images/PAD/available-pressed",
                new Transform2(new Vector2(600, 500), new Size2(200, 50)),
                () => { });

            _btnOrderFood = new ImageButton(
                "Images/PAD/orderfood-default",
                "Images/PAD/orderfood-hover",
                "Images/PAD/orderfood-pressed",
                new Transform2(new Vector2(600, 600), new Size2(200, 50)),
                () => { });

            _btnLogout = new ImageButton(
                "Images/PAD/logout-default",
                "Images/PAD/logout-hover",
                "Images/PAD/logout-pressed",
                new Transform2(new Vector2(600, 700), new Size2(200, 50)),
                () => World.NavigateToScene("Room"));

            _ui = new ClickUI();
            _ui.Add(_btnAvailableForCall);
            _ui.Add(_btnOrderFood);
            _ui.Add(_btnLogout);
        }

        public void Update(TimeSpan delta)
        {
            _ui.Update(delta);
        }

        public void Draw()
        {
            World.DrawBackgroundColor(Color.White);
            _btnAvailableForCall.Draw(Transform2.Zero);
            _btnOrderFood.Draw(Transform2.Zero);
            _btnLogout.Draw(Transform2.Zero);
        }
    }
}
