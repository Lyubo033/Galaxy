using System;
using System.Collections.Generic;

namespace GalaxyCatalog
{
    public class Galaxy : SpaceObject<Star>
    {
        protected Galaxy(string name, decimal age, GalaxyType type)
            : base(name)
        {
            Age = age;
            Type = type;
        }

        public decimal Age { get; private set; }
        public GalaxyType Type { get; private set; }

        public static Galaxy Create(string[] commandParts, int startPartIndex)
        {
            return new Galaxy(ExtractName(commandParts, startPartIndex, out int endingPartIndex), 
                            decimal.Parse(commandParts[endingPartIndex + 2]), 
                            Enum.Parse<GalaxyType>(commandParts[endingPartIndex + 1], true));
        }

        public override Dictionary<Type, int> Stats()
        {
            Dictionary<Type, int> stats = new Dictionary<Type, int>();
            stats.Add(typeof(Star), Children.Count);
            foreach (var star in Children)
            {
                var starStats = star.Value.Stats();
                foreach (var starStat in starStats)
                {
                    if (stats.ContainsKey(starStat.Key))
                    {
                        stats[starStat.Key] += starStat.Value;
                    }
                    else
                    {
                        stats.Add(starStat.Key, starStat.Value);
                    }
                }
            }

            return stats;
        }
    }
}
