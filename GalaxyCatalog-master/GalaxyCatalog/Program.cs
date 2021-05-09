using System;
using System.Collections.Generic;
using System.Linq;

namespace GalaxyCatalog
{
    class Program
    {
        static Dictionary<string, Galaxy> galaxyCatalog = new Dictionary<string, Galaxy>();

        static void Main(string[] args)
        {
            while (true)
            {
                var command = Console.ReadLine();
                var commandParts = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // skip empty command
                if (commandParts.Length == 0)
                    continue;

                switch (commandParts[0])
                {
                    case "add":
                        RunAddCommand(commandParts);
                        break;
                    case "stats":
                        Dictionary<Type, int> stats = new Dictionary<Type, int>();
                        stats.Add(typeof(Galaxy), galaxyCatalog.Count);
                        foreach (var galaxy in galaxyCatalog)
                        {
                            var galaxyStats = galaxy.Value.Stats();
                            foreach (var galaxyStat in galaxyStats)
                            {
                                if (stats.ContainsKey(galaxyStat.Key))
                                {
                                    stats[galaxyStat.Key] += galaxyStat.Value;
                                }
                                else
                                {
                                    stats.Add(galaxyStat.Key, galaxyStat.Value);
                                }
                            }
                        }

                        Print(stats);
                        break;
                    case "list":
                        ListSpaceObjects(commandParts[1]);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void ListSpaceObjects(string objectTypes)
        {
            switch (objectTypes)
            {
                case "galaxies":
                    Console.WriteLine("--- List of all researched galaxies ---");
                    Console.WriteLine(string.Join(", ", galaxyCatalog.Keys));
                    Console.WriteLine("--- End of galaxies list ---");
                    break;
                case "stars":
                    Console.WriteLine("--- List of all researched stars ---");
                    Console.WriteLine(string.Join(", ", galaxyCatalog.Values.SelectMany(x => x.GetChildren().Select(obj => obj.Name))));
                    Console.WriteLine("--- End of stars list ---");
                    break;
                case "planets":
                    Console.WriteLine("--- List of all researched planets ---");
                    Console.WriteLine(string.Join(", ", galaxyCatalog.Values.SelectMany(x => x.GetChildren().SelectMany(y=>y.GetChildren().Select(obj => obj.Name)))));
                    Console.WriteLine("--- End of planets list ---");
                    break;
                case "moons":
                    Console.WriteLine("--- List of all researched moons ---");
                    Console.WriteLine(string.Join(", ", galaxyCatalog.Values.SelectMany(x => x.GetChildren().SelectMany(x => x.GetChildren().SelectMany(x => x.GetChildren().Select(obj => obj.Name))))));
                    Console.WriteLine("--- End of moons list ---");
                    break;
                default:
                    break;
            }
        }

        private static void Print(Dictionary<Type, int> stats)
        {
            Console.WriteLine("--- Stats ---");
            Console.WriteLine($"Galaxies: {stats[typeof(Galaxy)]}");
            Console.WriteLine($"Stars: {stats[typeof(Star)]}");
            Console.WriteLine($"Planets: {stats[typeof(Planet)]}");
            Console.WriteLine($"Moons: {stats[typeof(Moon)]}");
            Console.WriteLine("--- End of stats ---");
        }

        private static void RunAddCommand(string[] commandParts)
        {
            switch (commandParts[1])
            {
                case "galaxy":
                    AddGalaxy(commandParts);
                    break;
                case "star":
                    AddStar(commandParts);
                    break;
                case "planet":
                    AddPlanet(commandParts);
                    break;
                case "moon":
                    AddMoon(commandParts);
                    break;
                default:
                    break;
            }
        }

        private static void AddMoon(string[] commandParts)
        {
            var planetName = SpaceObject.ExtractName(commandParts, 2, out int endingPartIndex);
            var galaxy = galaxyCatalog.Values.FirstOrDefault(x => x.HasChild(planetName));
            if (galaxy == null)
            {
                Console.WriteLine("Unknown planet.");
                return;
            }

            var planet = (Planet)galaxy.GetChild(planetName);
            var moon = Moon.Create(commandParts, endingPartIndex + 1);
            planet.AddChild(moon);
        }

        private static void AddPlanet(string[] commandParts)
        {
            var starName = SpaceObject.ExtractName(commandParts, 2, out int endingPartIndex);
            var galaxy = galaxyCatalog.Values.FirstOrDefault(x => x.HasChild(starName));
            if (galaxy == null)
            {
                Console.WriteLine("Unknown star.");
                return;
            }

            var star = (Star)galaxy.GetChild(starName);
            var planet = Planet.Create(commandParts, endingPartIndex + 1);
            star.AddChild(planet);
        }

        private static void AddStar(string[] commandParts)
        {
            var galaxyName = SpaceObject.ExtractName(commandParts, 2, out int endingPartIndex);
            var star = Star.Create(commandParts, endingPartIndex + 1);
            if (!galaxyCatalog.ContainsKey(galaxyName))
            {
                Console.WriteLine("Unknown galaxy.");
                return;
            }

            galaxyCatalog[galaxyName].AddChild(star);
        }

        private static void AddGalaxy(string[] commandParts)
        {
            var galaxy = Galaxy.Create(commandParts, 2);
            galaxyCatalog.Add(galaxy.Name, galaxy);
        }
    }
}
