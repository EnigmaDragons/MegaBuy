using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Common;

namespace MonoDragons.Core.MouseControls
{
    public sealed class MouseDragging : ISystem
    {
        private readonly List<GameObject> _targets = new List<GameObject>();

        private bool _leftButtonWasPressed;
        private Point _lastPos;

        public void Update(IEntities entities, TimeSpan delta)
        {
            var leftButtonIsPressed = Mouse.GetState().LeftButton == ButtonState.Pressed;
            var pos = Mouse.GetState().Position;

            if (StillDragging(leftButtonIsPressed))
                _targets.ForEach(t => t.Transform.Location += (pos - _lastPos).ToVector2());
            if (DragStarted(leftButtonIsPressed))
                SelectDragTarget(entities);
            if (!leftButtonIsPressed)
                _targets.Clear();

            _leftButtonWasPressed = leftButtonIsPressed;
            _lastPos = pos;
        }

        private void SelectDragTarget(IEntities entities)
        {
            var possibleTargets = new List<GameObject>();
            entities.ForEach(e => e.With<MouseDrag>(x => e.Transform.If(t => t.Intersects(_lastPos), t => possibleTargets.Add(e))));
            if (possibleTargets.Any())
                _targets.Add(possibleTargets.OrderByDescending(x => x.Transform.ZIndex).First());
        }

        private bool StillDragging(bool leftButtonIsPressed)
        {
            return leftButtonIsPressed && _leftButtonWasPressed;
        }

        private bool DragStarted(bool leftButtonIsPressed)
        {
            return leftButtonIsPressed && !_leftButtonWasPressed;
        }
    }
}
