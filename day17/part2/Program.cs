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
            //target area: x=25..67, y=-260..-200
            var parts = lines[0].Split(", ");
            var target = new Dictionary<int, (int, int)>();
            for (var p = 0; p < 2; p++)
            {
                var range = parts[p].Split('=')[1].Split("..");
                var min = int.Parse(range[0]);
                var max = int.Parse(range[1]);
                target[p] = (min, max);
            }
            var search = 1000;
            var result = 0;
            for (var x = 0; x < search; x++)
            {
                for (var y = -search; y < search; y++)
                {
                    var px = 0;
                    var py = 0;
                    var vx = x;
                    var vy = y;
                    var found = false;
                    var maxpy = 0;
                    while (true)
                    {
                        if (px >= target[0].Item1 && px <= target[0].Item2 && py >= target[1].Item1 && py <= target[1].Item2)
                        {
                            found = true;
                            break;
                        }
                        if (py < target[1].Item1 || px > target[0].Item2)
                        {
                            break;
                        }
                        px += vx;
                        py += vy;
                        maxpy = Math.Max(maxpy, py);
                        if (vx > 0) vx -= 1;
                        vy -= 1;
                    }
                    if (found)
                    {
                        result += 1;
                    }
                }
            }
            Console.WriteLine(result);
        }

    }
}
