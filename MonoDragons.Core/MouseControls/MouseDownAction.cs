using System;

namespace MonoDragons.Core.MouseControls
{
    public sealed class MouseDownAction
    {
        public Action Action { get; }
        public MouseButton Button { get; }

        public MouseDownAction(Action action, MouseButton button = MouseButton.Left)
        {
            Action = action;
            Button = button;
        }
    }
}
