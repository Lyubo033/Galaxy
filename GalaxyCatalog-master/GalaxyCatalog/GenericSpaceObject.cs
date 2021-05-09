using System.Collections.Generic;
using System.Linq;

namespace GalaxyCatalog
{
    public abstract class SpaceObject<T> : SpaceObject where T : SpaceObject
    {
        protected SpaceObject(string name) : base(name)
        {
            Children = new Dictionary<string, T>();
        }

        protected Dictionary<string, T> Children { get; set; }

        public void AddChild(T child)
        {
            if (child == null || child.Name == null)
                return;

            Children.Add(child.Name, child);
        }

        public override bool HasChild(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            var hasChild = Children.ContainsKey(name);
            // Check if any of the children has a child with the given name.
            if (!hasChild)
            {
                hasChild = Children.Values.Any(x => x.HasChild(name));
            }

            return hasChild;
        }

        public override SpaceObject GetChild(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || !HasChild(name))
                return null;

            SpaceObject child = null;
            foreach (var childEnumerator in Children)
            {
                if (childEnumerator.Key == name)
                {
                    child = childEnumerator.Value;
                    break;
                }

                child = childEnumerator.Value.GetChild(name);
                if (child != null)
                    break;
            }

            return child;
        }

        public override List<SpaceObject> GetChildren()
        {
            return Children.Select(x => (SpaceObject)x.Value).ToList();
        }
    }
}
