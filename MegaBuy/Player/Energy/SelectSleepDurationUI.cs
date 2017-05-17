﻿using System.Collections.Generic;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using System;

namespace MegaBuy.Player.Energy
{
    public sealed class SelectSleepDurationUI : IVisual
    {
        private readonly ColoredRectangle _backdrop;
        private readonly Label _question;
        private readonly List<ImageTextButton> _hourDurationOptions = new List<ImageTextButton>();
        private readonly Action _cancel;

        public ClickUIBranch Branch { get; }

        public SelectSleepDurationUI(Action cancel)
        {
            _cancel = cancel;
            Branch = new ClickUIBranch("Select Sleep Duration", (int)ClickUIPriorities.Room);
            _question = new Label {Transform = new Transform2(Sizes.Label), Text="How long would you like to sleep?"};
            _backdrop = new ColoredRectangle {Color = Color.Yellow, Transform = new Transform2(new Size2(600, 300))};
            _hourDurationOptions.Add(new ImageTextButton("10", "Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press",
                new Transform2(new Vector2(0, 100), Sizes.Button), () => GoToSleep(10)));
            _hourDurationOptions.Add(new ImageTextButton("8", "Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press",
                new Transform2(new Vector2(0, 175), Sizes.Button), () => GoToSleep(8)));
            _hourDurationOptions.Add(new ImageTextButton("6", "Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press",
                new Transform2(new Vector2(200, 100), Sizes.Button), () => GoToSleep(6)));
            _hourDurationOptions.Add(new ImageTextButton("4", "Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press",
                new Transform2(new Vector2(200, 175), Sizes.Button), () => GoToSleep(4)));
            _hourDurationOptions.Add(new ImageTextButton("2", "Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press",
                new Transform2(new Vector2(400, 100), Sizes.Button), () => GoToSleep(2)));
            _hourDurationOptions.Add(new ImageTextButton("1", "Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press",
                new Transform2(new Vector2(400, 175), Sizes.Button), () => GoToSleep(1)));
            _hourDurationOptions.Add(new ImageTextButton("Cancel", "Images/UI/button", "Images/UI/button-hover", "Images/UI/button-press",
                new Transform2(new Vector2(400, 15), Sizes.Button), () => _cancel()));
            _hourDurationOptions.ForEach(x => Branch.Add(x));
        }

        public void Draw(Transform2 parentTransform)
        {
            Branch.Location = parentTransform.Location;
            _backdrop.Draw(parentTransform);
            _question.Draw(parentTransform);
            _hourDurationOptions.ForEach(x => x.Draw(parentTransform));
        }

        private void GoToSleep(int hours)
        {
            World.Publish(new WentToBed(hours));
        }
    }
}
