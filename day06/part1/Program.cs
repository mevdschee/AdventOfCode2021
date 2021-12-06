using System;
using System.IO;
using System.Text.RegularExpressions;
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
            var fish = new List<int>();
            foreach (var num in lines[0].Split(','))
            {
                fish.Add(int.Parse(num));
            }
            var days = 256;
            for (var day = 0; day < days; day++)
            {
                var count = fish.Count;
                for (var i = 0; i < count; i++)
                {
                    switch (fish[i])
                    {
                        case 0:
                            fish[i] = 6;
                            fish.Add(8);
                            break;
                        default:
                            fish[i] -= 1;
                            break;
                    }
                }
            }
            Console.WriteLine(fish.Count);
        }
    }
}
