using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoDragons.Core.UserInterface
{
    public class ClickUIBranch
    {
        private List<ClickableUIElement> _elements = new List<ClickableUIElement>();
        public List<ClickUIBranch> SubBranches = new List<ClickUIBranch>();
        public readonly int Priority;
        private readonly string _name;
        private List<Action<ClickUIBranch>[]> subscriberActions = new List<Action<ClickUIBranch>[]>();

        private Vector2 parentLocation;
        public Vector2 ParentLocation
        {
            get { return parentLocation; }
            set
            {
                parentLocation = value;
                totalLocation = new Vector2(location.X + ParentLocation.X, location.Y + ParentLocation.Y);
                SubBranches.ForEach((b) => b.ParentLocation = totalLocation);
            }
        }
        private Vector2 location;
        public Vector2 Location
        {
            get { return location; }
            set
            {
                location = value;
                totalLocation = new Vector2(location.X + ParentLocation.X, location.Y + ParentLocation.Y);
                SubBranches.ForEach((b) => b.ParentLocation = totalLocation);
            }
        }
        private Vector2 totalLocation;

        public ClickUIBranch(string name, int priority)
        {
            Priority = priority;
            _name = name;
        }

        public void Add(ClickableUIElement element)
        {
            _elements.Add(element);
            _elements = _elements.OrderByDescending(x => x.Layer).ToList();
        }

        public void Remove(ClickableUIElement element)
        {
            _elements.Remove(element);
        }

        public void Subscribe(Action<ClickUIBranch>[] actions)
        {
            subscriberActions.Add(actions);
        }

        public void Unsubscribe(Action<ClickUIBranch>[] actions)
        {
            subscriberActions.Remove(actions);
        }

        public void Add(ClickUIBranch branch)
        {
            branch.ParentLocation = new Vector2(ParentLocation.X + Location.X, ParentLocation.Y + Location.Y);
            SubBranches.Add(branch);
            subscriberActions.ForEach((a) => a[0](branch));
        }

        public void Remove(ClickUIBranch branch)
        {
            SubBranches.Remove(branch);
            subscriberActions.ForEach((a) => a[1](branch));
        }

        public void ClearElements()
        {
            _elements.Clear();
        }

        public ClickableUIElement GetElement(Point mousePosition)
        {
            var element = _elements.FirstOrDefault(x => x.Area.Contains(mousePosition - totalLocation.ToPoint()));
            return element ?? ClickUI.None;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
