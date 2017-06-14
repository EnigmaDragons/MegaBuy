using System;
using System.Collections.Generic;
using MegaBuy.UIs;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.UserInterface.Layouts;

namespace MegaBuy.Player.Energy
{
    public class NewSelectSleepDurationUI : IVisualControl
    {
        private readonly GridLayout _grid;
        private readonly ImageLabel _timeLabel;

        private int _hours = 0;

        public ClickUIBranch Branch { get; }

        public NewSelectSleepDurationUI(Action cancel)
        {
            _grid = new GridLayout(new Size2(1600, 900), 1, 1);
            var innerGrid = new GridLayout(new Size2(250, 525), new List<Definition>
            {
                new ConcreteDefinition(25),
                new ConcreteDefinition(200),
                new ConcreteDefinition(25),
            }, new List<Definition>
            {
                new ConcreteDefinition(25),
                new ConcreteDefinition(50),
                new ConcreteDefinition(25),
                new ConcreteDefinition(70),
                new ConcreteDefinition(10),
                new ConcreteDefinition(50),
                new ConcreteDefinition(10),
                new ConcreteDefinition(70),
                new ConcreteDefinition(25),
                new ConcreteDefinition(70),
                new ConcreteDefinition(25),
                new ConcreteDefinition(70),
                new ConcreteDefinition(25)
            });

            var summary = new Label
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
                Font = "Fonts/16",
                Transform = new Transform2(innerGrid.GetBlockSize(1, 2, 4, 1)),
                Text = "How long do you want to sleep for?"
            };
            var upHour = ImageTextButtonFactory.CreateUpArrow(Vector2.Zero, AddHour);
            var smartUpHour = new SmartControl(upHour, (int)ClickUIPriorities.Room);
            var downHour = ImageTextButtonFactory.CreateDownArrow(Vector2.Zero, ReduceHour);
            var smartDownHour = new SmartControl(downHour, (int)ClickUIPriorities.Room);
            _timeLabel = new ImageLabel(_hours + " Hours", "Images/UI/label-small", new Transform2(Sizes.SmallLabel));
            var sleepButton = ImageTextButtonFactory.Create("Sleep", Vector2.Zero, () => World.Publish(new WentToBed(_hours)));
            var smartSleepButton = new SmartControl(sleepButton, (int)ClickUIPriorities.Room);
            var cancelButton = ImageTextButtonFactory.Create("Cancel", Vector2.Zero, cancel);
            var smartCancelButton = new SmartControl(cancelButton, (int)ClickUIPriorities.Room);

            _grid.AddSpatial(innerGrid, new Transform2(innerGrid.Size), 1, 1);
            innerGrid.AddSpatial(new ImageBox(new Transform2(innerGrid.Size), "Images/UI/messenger"), new Transform2(innerGrid.Size), 1, 1, 3, 13);
            innerGrid.AddSpatial(summary, summary.Transform, 2, 2);
            innerGrid.AddSpatial(smartUpHour, new Transform2(new Size2(70, 70)), 2, 4);
            innerGrid.AddSpatial(_timeLabel, new Transform2(Sizes.SmallLabel), 2, 6);
            innerGrid.AddSpatial(smartDownHour, new Transform2(new Size2(70, 70)), 2, 8);
            innerGrid.AddSpatial(smartSleepButton, sleepButton.Transform, 2, 10);
            innerGrid.AddSpatial(smartCancelButton, cancelButton.Transform, 2, 12);
            Branch = new ClickUIBranch("Sleep Duration", (int)ClickUIPriorities.Room);
            Branch.Add(smartUpHour.Branch);
            Branch.Add(smartDownHour.Branch);
            Branch.Add(smartSleepButton.Branch);
            Branch.Add(smartCancelButton.Branch);
        }

        public void Draw(Transform2 parentTransform)
        {
            _grid.Draw(parentTransform);
        }

        private void AddHour()
        {
            _hours++;
            _timeLabel.Text = _hours + " Hours";
        }

        private void ReduceHour()
        {
            if (_hours == 0)
                return;
            _hours--;
            _timeLabel.Text = _hours + " Hours";
        }
    }
}
