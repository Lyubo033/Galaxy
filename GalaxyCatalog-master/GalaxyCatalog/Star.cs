using System;
using System.Collections.Generic;

namespace GalaxyCatalog
{
    public class Star : SpaceObject<Planet>
    {
        public Star(string name, decimal mass, decimal radius, decimal temperture, decimal brightness)
            : base(name)
        {
            Mass = mass;
            Radius = radius;
            Temperture = temperture;
            Brightness = brightness;
        }

        public StarClass Class { get; private set; }
        public decimal Mass { get; private set; }
        public decimal Radius { get; private set; }
        public decimal Temperture { get; private set; }
        public decimal Brightness { get; private set; }


        public static Star Create(string[] commandParts, int startPartIndex)
        {
            return new Star(ExtractName(commandParts, startPartIndex, out int endingPartIndex), 
                            decimal.Parse(commandParts[endingPartIndex + 1]), 
                            decimal.Parse(commandParts[endingPartIndex + 2]), 
                            decimal.Parse(commandParts[endingPartIndex + 3]), 
                            decimal.Parse(commandParts[endingPartIndex + 4]));
        }

        public override Dictionary<Type, int> Stats()
        {
            Dictionary<Type, int> stats = new Dictionary<Type, int>();
            stats.Add(typeof(Planet), Children.Count);
            foreach (var planet in Children)
            {
                var planetStats = planet.Value.Stats();
                foreach (var planetStat in planetStats)
                {
                    if (stats.ContainsKey(planetStat.Key))
                    {
                        stats[planetStat.Key] += planetStat.Value;
                    }
                    else
                    {
                        stats.Add(planetStat.Key, planetStat.Value);
                    }
                }
            }

            return stats;
        }
    }
}
