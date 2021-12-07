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
            var crabs = new List<int>();
            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (var num in lines[0].Split(','))
            {
                var number = int.Parse(num);
                min = Math.Min(min, number);
                max = Math.Max(max, number);
                crabs.Add(number);
            }
            var result = int.MaxValue;
            for (var pos = min; pos <= max; pos++)
            {
                var cost = 0;
                for (var i = 0; i < crabs.Count; i++)
                {
                    cost += Math.Abs(crabs[i]-pos);
                }
                result = Math.Min(result, cost);
            }
            Console.WriteLine(result);
        }
    }
}
