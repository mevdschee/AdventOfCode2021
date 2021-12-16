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
            for (var y = 0; y < lines.Count; y++)
            {
                var strLength = lines[y].Length;
                for (var i = 1; i < 5; i++)
                {
                    for (var x = 0; x < strLength; x++)
                    {
                        var n = int.Parse(new string(lines[y][x], 1)) + i;
                        n = n > 9 ? (n - 1) % 9 + 1 : n;
                        lines[y] += n.ToString();
                    }
                }
            }
            var lineCount = lines.Count;
            for (var i = 1; i < 5; i++)
            {
                for (var y = 0; y < lineCount; y++)
                {
                    var line = "";
                    for (var x = 0; x < lines[y].Length; x++)
                    {
                        var n = int.Parse(new string(lines[y][x], 1)) + i;
                        n = n > 9 ? (n - 1) % 9 + 1 : n;
                        line += n.ToString();
                    }
                    lines.Add(line);
                }
            }
            var width = lines[0].Length;
            var height = lines.Count;
            var distances = new Dictionary<(int, int), int>();
            var front = new List<(int, int)>();
            distances.Add((0, 0), 0);
            front.Add((0, 0));
            while (front.Count > 0)
            {
                var newFront = new List<(int, int)>();
                foreach (var (x, y) in front)
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
                            var distance = int.Parse(new string(lines[ny][nx], 1));
                            if (distances.ContainsKey((nx, ny)) && distances[(nx, ny)] <= distances[(x, y)] + distance)
                            {
                                continue;
                            }
                            distances[(nx, ny)] = distances[(x, y)] + distance;
                            newFront.Add((nx, ny));
                        }
                    }
                }
                front = newFront;
            }
            Console.WriteLine(distances[(width - 1, height - 1)]);
        }
    }
}
