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
            var number = "";
            var inverted = "";
            var len = lines[0].Length;
            for (int i = 0; i < len; i++)
            {
                int zeros = 0;
                int ones = 0;
                foreach (var line in lines)
                {
                    if (line[i] == '0')
                    {
                        zeros += 1;
                    }
                    else
                    {
                        ones += 1;
                    }
                }
                number += zeros > ones ? '0' : '1';
                inverted += zeros > ones ? '1' : '0';
            }
            Console.WriteLine(Convert.ToInt64(number, 2) * Convert.ToInt64(inverted, 2));
        }
    }
}
