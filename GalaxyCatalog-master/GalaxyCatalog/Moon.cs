using System;
using System.Collections.Generic;

namespace GalaxyCatalog
{
    public class Moon : SpaceObject
    {
        public Moon(string name)
            : base(name)
        {

        }

        public static Moon Create(string[] commandParts, int startPartIndex)
        {
            return new Moon(ExtractName(commandParts, startPartIndex, out int endingPartIndex));
        }

        public override SpaceObject GetChild(string name)
        {
            throw new System.NotImplementedException();
        }

        public override List<SpaceObject> GetChildren()
        {
            throw new NotImplementedException();
        }

        public override bool HasChild(string name)
        {
            throw new System.NotImplementedException();
        }

        public override Dictionary<Type, int> Stats()
        {
            throw new NotImplementedException();
        }
    }
}
