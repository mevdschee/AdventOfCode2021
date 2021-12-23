using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Program
{
    class Program
    {
        class Cuboid
        {
            public int x1;
            public int x2;
            public int y1;
            public int y2;
            public int z1;
            public int z2;

            public Cuboid(int x1, int x2, int y1, int y2, int z1, int z2)
            {
                this.x1 = x1;
                this.x2 = x2;
                this.y1 = y1;
                this.y2 = y2;
                this.z1 = z1;
                this.z2 = z2;
            }

            public List<Cuboid> Subtract(Cuboid other)
            {
                var cuboids = new List<Cuboid>();

                if (this.x1 < other.x2 && this.x2 > other.x1 && this.y1 < other.y2 && this.y2 > other.y1 && this.z1 < other.z2 && this.z2 > other.z1)
                {
                    var borders = new Cuboid(Math.Min(Math.Max(other.x1, this.x1), this.x2), Math.Min(Math.Max(other.x2, this.x1), this.x2),
                                             Math.Min(Math.Max(other.y1, this.y1), this.y2), Math.Min(Math.Max(other.y2, this.y1), this.y2),
                                             Math.Min(Math.Max(other.z1, this.z1), this.z2), Math.Min(Math.Max(other.z2, this.z1), this.z2));
                    cuboids.Add(new Cuboid(this.x1, borders.x1, this.y1, this.y2, this.z1, this.z2));
                    cuboids.Add(new Cuboid(borders.x2, this.x2, this.y1, this.y2, this.z1, this.z2));
                    cuboids.Add(new Cuboid(borders.x1, borders.x2, this.y1, borders.y1, this.z1, this.z2));
                    cuboids.Add(new Cuboid(borders.x1, borders.x2, borders.y2, this.y2, this.z1, this.z2));
                    cuboids.Add(new Cuboid(borders.x1, borders.x2, borders.y1, borders.y2, this.z1, borders.z1));
                    cuboids.Add(new Cuboid(borders.x1, borders.x2, borders.y1, borders.y2, borders.z2, this.z2));
                }
                else
                {
                    cuboids.Add(this);
                }
                var result = new List<Cuboid>();
                foreach (var cuboid in cuboids)
                {
                    if (cuboid.Volume() > 0)
                    {
                        result.Add(cuboid);
                    }
                }
                return result;
            }

            public long Volume()
            {
                long width = Math.Abs(x2 - x1);
                long depth = Math.Abs(y2 - y1);
                long height = Math.Abs(z2 - z1);
                return width * depth * height;
            }
        }

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
            var steps = new List<(string, Cuboid)>();
            foreach (var line in lines)
            {
                var status = line.Split(" ")[0];
                var parts = line.Split(" ")[1].Split(",");
                int x1 = 0, x2 = 0, y1 = 0, y2 = 0, z1 = 0, z2 = 0;
                foreach (var part in parts)
                {
                    var dimension = part.Split("=")[0][0];
                    var range = part.Split("=")[1].Split("..");
                    switch (dimension)
                    {
                        case 'x':
                            x1 = int.Parse(range[0]);
                            x2 = int.Parse(range[1]) + 1;
                            break;
                        case 'y':
                            y1 = int.Parse(range[0]);
                            y2 = int.Parse(range[1]) + 1;
                            break;
                        case 'z':
                            z1 = int.Parse(range[0]);
                            z2 = int.Parse(range[1]) + 1;
                            break;
                    }
                }
                steps.Add((status, new Cuboid(x1, x2, y1, y2, z1, z2)));
            }
            var cuboids = new List<Cuboid>();
            foreach (var (status, cuboid) in steps)
            {
                var newCuboids = new List<Cuboid>();
                foreach (var other in cuboids)
                {
                    var add = other.Subtract(cuboid);
                    foreach (var c in add)
                    {
                        newCuboids.Add(c);
                    }
                }
                if (status == "on")
                {
                    newCuboids.Add(cuboid);
                }
                cuboids = newCuboids;
            }
            long sum = 0;
            foreach (var c in cuboids)
            {
                sum += c.Volume();

            }
            Console.WriteLine(sum);
        }
    }
}
