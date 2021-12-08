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
                    if (line.Length>0) 
                    {
                        lines.Add(line);
                    }
                }
            }
            var lengths = new Dictionary<int, int>();
            foreach (var line in lines)
            {
                var parts = line.Split(" | ",2);
                foreach (var number in parts[1].Split(' '))
                {
                    lengths[number.Length] = lengths.GetValueOrDefault(number.Length, 0) + 1;
                }
            }
            var result = lengths[2] + lengths[3] + lengths[4] + lengths[7];
            Console.WriteLine(result);
        }
    }
}
