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
                    lines.Add(line);
                }
            }
            var height = 0;
            var width = 0;
            var field = new Dictionary<(int, int), bool>();
            var folds = new List<(char, int)>();
            var dots = true;
            foreach (var line in lines)
            {
                if (line == "")
                {
                    dots = false;
                    continue;
                }
                if (dots)
                {
                    var words = line.Split(',');
                    var x = int.Parse(words[0]);
                    var y = int.Parse(words[1]);
                    width = Math.Max(width, x);
                    height = Math.Max(height, y);
                    field[(x, y)] = true;
                }
                else
                {
                    var words = line.Split('=');
                    var c = words[0][words[0].Length - 1];
                    var pos = int.Parse(words[1]);
                    folds.Add((c, pos));
                }
            }
            foreach (var (axis, pos) in folds)
            {
                if (axis == 'x')
                {
                    for (var y = 0; y < height; y++)
                    {
                        field.Remove((pos, y));
                        for (var i = 0; i < pos; i++)
                        {
                            var lx = pos - 1 - i;
                            var rx = pos + 1 + i;
                            if (field.ContainsKey((rx, y)))
                            {
                                field[(lx, y)] = field[(rx, y)];
                                field.Remove((rx, y));
                            }
                        }
                        width = Math.Max(pos, width - 1 - pos);
                    }
                }
                else
                {
                    for (var x = 0; x < width; x++)
                    {
                        field.Remove((x, pos));
                        for (var i = 0; i < pos; i++)
                        {
                            var ty = pos - 1 - i;
                            var by = pos + 1 + i;
                            if (field.ContainsKey((x, by)))
                            {
                                field[(x, ty)] = field[(x, by)];
                                field.Remove((x, by));
                            }

                        }
                        height = Math.Max(pos, height - 1 - pos);
                    }
                }
            }
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    Console.Write(field.ContainsKey((x,y))?'#':' ');
                }
                Console.WriteLine();
            }
        }
    }
}