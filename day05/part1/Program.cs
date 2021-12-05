using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

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
            var dangers = new Dictionary<Point, int>();
            foreach (var line in lines)
            {
                var coordinates = line.Split(" -> ");
                var start = coordinates[0].Split(',');
                var end = coordinates[1].Split(',');
                var startx = int.Parse(start[0]);
                var starty = int.Parse(start[1]);
                var endx = int.Parse(end[0]);
                var endy = int.Parse(end[1]);
                if (startx != endx && starty!=endy) {
                    continue;
                }
                var length = Math.Max(Math.Abs(endx-startx),Math.Abs(endy-starty));
                for (var i = 0; i<=length; i++)
                {
                    var x = startx;
                    if (endx<startx) {
                        x -= i;
                    } else if (endx>startx) {
                        x += i;
                    }
                    var y = starty;
                    if (endy<starty) {
                        y -= i;
                    } else if (endy>starty) {
                        y += i;
                    }
                    var point = new Point(x, y);
                    var count = dangers.GetValueOrDefault(point, 0);
                    dangers[point] = count + 1;
                }
            }
            var result = 0;
            foreach (var item in dangers)
            {
                if (item.Value > 1)
                {
                    result += 1;
                }
            }
            Console.WriteLine(result);
        }
    }
}
