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
                    if (line.Length > 0)
                    {
                        lines.Add(line);
                    }
                }
            }
            var numbers = new List<string>{
                "abcefg",
                "cf",
                "acdeg",
                "acdfg",
                "bcdf",
                "abdfg",
                "abdefg",
                "acf",
                "abcdefg",
                "abcdfg",
            };
            var knownLengths = new Dictionary<int, int>
            {
                {2, 1},
                {3, 7},
                {4, 4},
                {7, 8},
            };
            var knownFrequencies = new Dictionary<int, char>
            {
                {6, 'b'},
                {4, 'e'},
                {9, 'f'},
            };
            var result = 0;
            foreach (var line in lines)
            {
                var parts = line.Split(" | ", 2);
                var segments = new Dictionary<char, string>();
                foreach (var segment in numbers[8])
                {
                    segments[segment] = numbers[8];
                }
                foreach (var number in parts[0].Split(' '))
                {
                    foreach (var knownLength in knownLengths)
                    {
                        if (number.Length == knownLength.Key)
                        {
                            foreach (var segment in numbers[8])
                            {
                                if (!numbers[knownLength.Value].Contains(segment))
                                {
                                    foreach (var c in number)
                                    {
                                        segments[c] = segments[c].Replace(segment, ' ').Replace(" ", "");
                                    }
                                }
                            }
                        }
                    }
                }
                var frequencies = new Dictionary<char, int>();
                foreach (var number in parts[0].Split(' '))
                {
                    foreach (var c in number)
                    {
                        frequencies[c] = frequencies.GetValueOrDefault(c, 0) + 1;
                    }
                }
                foreach (var frequency in frequencies)
                {
                    if (knownFrequencies.ContainsKey(frequency.Value))
                    {
                        var segment = knownFrequencies[frequency.Value];
                        segments[frequency.Key] = "" + segment;
                    }
                }
                for (var i = 0; i < 8; i++)
                {
                    foreach (var value in segments.Values)
                    {
                        if (value.Length == 1)
                        {
                            foreach (var key in segments.Keys)
                            {
                                if (segments[key].Length != 1)
                                {
                                    segments[key] = segments[key].Replace(value[0], ' ').Replace(" ", "");
                                }
                            }
                        }
                    }
                }
                var mapping = new Dictionary<char, char>();
                foreach (var item in segments) 
                {
                    mapping[item.Key] = item.Value[0];
                }
                var digits = new List<int>();
                foreach (var number in parts[1].Split(' '))
                {
                    var characters = number.ToCharArray();
                    for (var i=0; i<characters.Length; i++)
                    {
                        characters[i] = mapping[characters[i]];
                    }
                    Array.Sort(characters);
                    var sorted = new string(characters);
                    for(var n=0;n<numbers.Count;n++) {
                        if (sorted == numbers[n])
                        {
                            digits.Add(n);
                        }
                    }
                }
                var power = 1;
                digits.Reverse();
                foreach (var digit in digits)
                {
                    result += digit * power;
                    power*=10;
                }
            }
            Console.WriteLine(result);
        }
    }
}
