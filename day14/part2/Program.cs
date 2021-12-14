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
            var rules = new Dictionary<(char, char), char>();
            var polymer = "";
            foreach (var line in lines)
            {
                if (polymer == "")
                {
                    polymer = line;
                    continue;
                }
                if (line == "")
                {
                    continue;
                }
                var words = line.Split(" -> ");
                rules.Add((words[0][0], words[0][1]), words[1][0]);
            }
            for (var step = 0; step < 10; step++)
            {
                //Console.WriteLine(step);
                var newPolymer = "";
                for (var i = 0; i < polymer.Length - 1; i++)
                {
                    newPolymer += polymer[i];
                    foreach (var (first, second) in rules.Keys)
                    {
                        if (polymer[i] == first && polymer[i + 1] == second)
                        {
                            newPolymer += rules[(first, second)];
                        }
                    }
                }
                newPolymer += polymer[polymer.Length - 1];
                polymer = newPolymer;
            }
            var counts = new Dictionary<char, long>();
            for (var i = 0; i < polymer.Length; i++)
            {
                var count = counts.GetValueOrDefault(polymer[i], 0);
                counts[polymer[i]] = count + 1;
            }
            var frequencies = new List<long>(counts.Values);
            frequencies.Sort();
            Console.WriteLine(frequencies[frequencies.Count - 1] - frequencies[0]);
        }
    }
}
