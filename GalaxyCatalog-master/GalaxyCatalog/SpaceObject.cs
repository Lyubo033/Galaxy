using System;
using System.Collections.Generic;
using System.Linq;

namespace GalaxyCatalog
{
    public abstract class SpaceObject
    {
        protected SpaceObject(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public static string ExtractName(string[] commandParts, int startingPartIndex, out int endingPartIndex)
        {
            // Handle mutiple intervals in name
            endingPartIndex = startingPartIndex;
            for (int i = startingPartIndex; i < commandParts.Length; i++)
            {
                if (commandParts[i].EndsWith("]"))
                {
                    endingPartIndex = i;
                    break;
                }
            }

            return string.Join(" ", commandParts.Skip(startingPartIndex)
                                                .Take(endingPartIndex - startingPartIndex + 1))
                        .Trim('[', ']');
        }

        public abstract bool HasChild(string name);
        public abstract SpaceObject GetChild(string name);
        public abstract Dictionary<Type, int> Stats();
        public abstract List<SpaceObject> GetChildren();
    }
}
