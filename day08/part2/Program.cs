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
                {2, 1}, // two segments make number 1
                {3, 7}, 
                {4, 4},
                {7, 8},
            };
            var knownFrequencies = new Dictionary<int, char>
            {
                {6, 'b'}, // numbers 0-9 have 6 times segment b
                {4, 'e'},
                {9, 'f'},
            };
            var result = 0;
            foreach (var line in lines)
            {
                var parts = line.Split(" | ", 2);
                // count the frequencies in the first part of the string (before the pipe)
                var frequencies = new Dictionary<char, int>();
                foreach (var number in parts[0].Split(' '))
                {
                    foreach (var c in number)
                    {
                        frequencies[c] = frequencies.GetValueOrDefault(c, 0) + 1;
                    }
                }
                // create a segments dictionary holding the mapping options
                var segments = new Dictionary<char, string>();
                foreach (var segment in numbers[8])
                {
                    var frequency = frequencies[segment];
                    if (knownFrequencies.ContainsKey(frequency))
                    {
                        var onlyOption = knownFrequencies[frequency];
                        segments[segment] = new string(onlyOption, 1);
                    }
                    else
                    {
                        segments[segment] = numbers[8];
                    }
                }
                // walk over each number ruling out segment mappings based on known lengths
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
                // walk max 7 times over each segment mapping to see whether they can be reduced
                for (var i = 0; i < numbers[8].Length; i++)
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
                // the mapping is found, now store it in a dictionary
                var mapping = new Dictionary<char, char>();
                foreach (var item in segments)
                {
                    mapping[item.Key] = item.Value[0];
                }
                // walk over the second part (after the pipe) and match the mapped digits
                var digits = "";
                foreach (var number in parts[1].Split(' '))
                {
                    var characters = number.ToCharArray();
                    for (var i = 0; i < characters.Length; i++)
                    {
                        characters[i] = mapping[characters[i]];
                    }
                    Array.Sort(characters);
                    var sorted = new string(characters);
                    for (var n = 0; n < numbers.Count; n++)
                    {
                        if (sorted == numbers[n])
                        {
                            digits += n;
                        }
                    }
                }
                result += int.Parse(digits);
            }
            Console.WriteLine(result);
        }
    }
}
