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
            var polymerString = "";
            foreach (var line in lines)
            {
                if (polymerString == "")
                {
                    polymerString = line;
                    continue;
                }
                if (line == "")
                {
                    continue;
                }
                var words = line.Split(" -> ");
                rules.Add((words[0][0], words[0][1]), words[1][0]);
            }
            var polymer = new Dictionary<(char, char), long>();
            for (var i = 0; i < polymerString.Length - 1; i++)
            {
                var key = (polymerString[i], polymerString[i + 1]);
                var count = polymer.GetValueOrDefault(key, 0);
                polymer[key] = count + 1;
            }
            for (var step = 0; step < 4; step++)
            {
                Console.WriteLine(step);
                var newPolymer = new Dictionary<(char, char), long>();
                foreach (var pair in polymer.Keys)
                {
                    foreach (var (first, second) in rules.Keys)
                    {
                        if (pair[0] == first && pair[1] == second)
                        {
                            var insert = rules[(first, second)];
                            var count1 = newPolymer.GetValueOrDefault((first, insert), 0);
                            newPolymer[(first, insert)] = count1 + polymer[pair];
                            var count2 = newPolymer.GetValueOrDefault((insert, second), 0);
                            newPolymer[(insert, second)] = count2 + polymer[pair];
                        }
                    }
                }
            }
            var counts = new Dictionary<char, long>();
            foreach (var pair in polymer.Keys)
            {
                var count = counts.GetValueOrDefault(pair[0], 0);
                counts[pair[0]] = count + polymer[pair];
            }
            var frequencies = new List<long>(counts.Values);
            frequencies.Sort();
            Console.WriteLine(frequencies[frequencies.Count - 1] - frequencies[0]);
        }
    }
}
