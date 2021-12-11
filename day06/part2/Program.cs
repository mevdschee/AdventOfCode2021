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
            var counts = new Dictionary<int, long>();
            foreach (var num in lines[0].Split(','))
            {
                var number = int.Parse(num);
                var count = counts.GetValueOrDefault(number, 0);
                counts[number] = count + 1;
            }
            var days = 256;
            for (var day = 0; day < days; day++)
            {
                for (var i = 0; i <= 8; i++)
                {
                    counts[i - 1] = counts.GetValueOrDefault(i, 0);
                }
                counts[6] += counts[-1];
                counts[8] = counts[-1];
            }
            long result = 0;
            for (var i = 0; i <= 8; i++)
            {
                result += counts[i];
            }
            Console.WriteLine(result);
        }
    }
}
