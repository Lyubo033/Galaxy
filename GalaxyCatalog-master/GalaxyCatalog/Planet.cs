using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyCatalog
{
    public class Planet : SpaceObject<Moon>
    {
        public Planet(string name, string type, bool isInhabitant)
            : base(name)
        {
            IsInhabitant = isInhabitant;
            switch (type)
            {
                case "terrestrial":
                    Type = PlanetType.Terrestrial;
                    break;
                case "giant planet":
                    Type = PlanetType.GiantPlanet;
                    break;
                case "mesoplanet":
                    Type = PlanetType.Mesoplanet;
                    break;
                case "mini-neptune":
                    Type = PlanetType.MiniNeptune;
                    break;
                case "Planetar":
                    Type = PlanetType.Planetar;
                    break;
                case "super-earth":
                    Type = PlanetType.SuperEarth;
                    break;
                case "super-jupiter":
                    Type = PlanetType.SuperJupiter;
                    break;
                case "sub-earth":
                    Type = PlanetType.SubEarth;
                    break;
            }
        }

        public PlanetType Type { get; private set; }
        public bool IsInhabitant { get; private set; }

        public static Planet Create(string[] commandParts, int startPartIndex)
        {
            var name = ExtractName(commandParts, startPartIndex, out int endingPartIndex);
            var isInhabitant = commandParts[endingPartIndex + 2].ToLower() == "yes";
            return new Planet(name, commandParts[endingPartIndex + 1], isInhabitant);
        }

        public override Dictionary<Type, int> Stats()
        {
            Dictionary<Type, int> stats = new Dictionary<Type, int>();
            stats.Add(typeof(Moon), Children.Count);
            return stats;
        }
    }
}
