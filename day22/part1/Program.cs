using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = new List<string>();
            using (StreamReader reader = new StreamReader("input"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length > 0)
                    {
                        lines.Add(line);
                    }
                }
            }
            var steps = new List<(string, Dictionary<char, (int, int)>)>();
            foreach (var line in lines)
            {
                var status = line.Split(" ")[0];
                var parts = line.Split(" ")[1].Split(",");
                var ranges = new Dictionary<char, (int, int)>();
                foreach (var part in parts)
                {
                    var dimension = part.Split("=")[0][0];
                    var range = part.Split("=")[1].Split("..");
                    var rangeMin = int.Parse(range[0]);
                    var rangeMax = int.Parse(range[1]);
                    ranges.Add(dimension, (rangeMin, rangeMax));
                }
                steps.Add((status, ranges));
            }
            var cubes = new Dictionary<(int, int, int), bool>();
            foreach (var (status, ranges) in steps)
            {
                var startX = Math.Max(-50, ranges['x'].Item1);
                var endX = Math.Min(50, ranges['x'].Item2);
                var startY = Math.Max(-50, ranges['y'].Item1);
                var endY = Math.Min(50, ranges['y'].Item2);
                var startZ = Math.Max(-50, ranges['z'].Item1);
                var endZ = Math.Min(50, ranges['z'].Item2);

                if (status == "on")
                {
                    for (var x = startX; x <= endX; x++)
                    {
                        for (var y = startY; y <= endY; y++)
                        {
                            for (var z = startZ; z <= endZ; z++)
                            {
                                cubes[(x, y, z)] = true;
                            }
                        }
                    }
                }
                else
                {
                    for (var x = startX; x <= endX; x++)
                    {
                        for (var y = startY; y <= endY; y++)
                        {
                            for (var z = startZ; z <= endZ; z++)
                            {
                                cubes.Remove((x, y, z));
                            }
                        }
                    }

                }
            }
            Console.WriteLine(cubes.Count);
        }
    }
}
