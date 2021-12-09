using System;
using System.IO;
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
            var width = lines[0].Length;
            var height = lines.Count;
            var depths = new Dictionary<(int, int), int>();
            for (var y = 0; y < height; y++)
            {
                var line = lines[y];
                var characters = line.ToCharArray();
                for (var x = 0; x < width; x++)
                {
                    var depth = int.Parse(line.Substring(x, 1));
                    depths[(x, y)] = depth;
                }
            }
            var basins = new Dictionary<(int, int), HashSet<(int, int)>>();
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var count = 0;
                    var lower = 0;
                    for (var dy = -1; dy <= 1; dy++)
                    {
                        for (var dx = -1; dx <= 1; dx++)
                        {
                            var nx = x + dx;
                            var ny = y + dy;
                            if ((ny < 0 || ny >= height) || (nx < 0 || nx >= width))
                            {
                                continue;
                            }
                            if (dx != 0 && dy != 0)
                            {
                                continue;
                            }
                            if (nx == x && ny == y)
                            {
                                continue;
                            }
                            count += 1;
                            if (depths[(x, y)] < depths[(nx, ny)])
                            {
                                lower += 1;
                            }
                        }
                    }
                    if (count == lower)
                    {
                        var coords = new HashSet<(int, int)>();
                        coords.Add((x, y));
                        basins.Add((x, y), coords);
                    }
                }
            }
            var changed = true;
            while (changed)
            {
                changed = false;
                foreach (var coords in basins.Values)
                {
                    var additions = new HashSet<(int, int)>();
                    foreach (var (x, y) in coords)
                    {
                        for (var dy = -1; dy <= 1; dy++)
                        {
                            for (var dx = -1; dx <= 1; dx++)
                            {
                                var nx = x + dx;
                                var ny = y + dy;
                                if ((ny < 0 || ny >= height) || (nx < 0 || nx >= width))
                                {
                                    continue;
                                }
                                if (dx != 0 && dy != 0)
                                {
                                    continue;
                                }
                                if (nx == x && ny == y)
                                {
                                    continue;
                                }
                                if (coords.Contains((nx, ny)))
                                {
                                    continue;
                                }
                                if (depths[(nx, ny)] < 9)
                                {
                                    additions.Add((nx, ny));
                                }
                            }
                        }
                    }
                    foreach (var addition in additions)
                    {
                        if (!coords.Contains(addition))
                        {
                            coords.Add(addition);
                            changed = true;
                        }
                    }
                }
            }
            var sizes = new List<int>();
            foreach (var coords in basins.Values)
            {
                sizes.Add(coords.Count);
            }
            sizes.Sort();
            sizes.Reverse();
            var result = 1;
            for (var i = 0; i < 3; i++)
            {
                result *= sizes[i];
            }
            Console.WriteLine(result);
        }
    }
}
