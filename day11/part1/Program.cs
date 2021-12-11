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
            var height = lines.Count;
            var width = lines[0].Length;
            var field = new Dictionary<(int, int), int>();
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    field[(x, y)] = int.Parse(new string(lines[y][x], 1));
                }
            }
            var result = 0;
            for (var i=0;i<100;i++)
            {
                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        if (field[(x, y)] < 10)
                        {
                            field[(x, y)] += 1;
                        }
                    }
                }
                var flashed = true;
                while (flashed)
                {
                    flashed = false;
                    for (var y = 0; y < height; y++)
                    {
                        for (var x = 0; x < width; x++)
                        {
                            if (field[(x, y)] == 10)
                            {
                                flashed = true;
                                for (var dy = -1; dy <= 1; dy++)
                                {
                                    for (var dx = -1; dx <= 1; dx++)
                                    {
                                        var nx = x + dx;
                                        var ny = y + dy;
                                        if (nx == x && ny == y)
                                        {
                                            field[(x, y)] = 0;
                                            result += 1;
                                        }
                                        else
                                        {
                                            if (field.ContainsKey((nx, ny)))
                                            {
                                                if (field[(nx, ny)] < 10 && field[(nx, ny)] > 0)
                                                {
                                                    field[(nx, ny)] += 1;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine(result);
        }
    }
}
