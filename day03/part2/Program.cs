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
            var len = lines[0].Length;
            var majLines = lines;
            for (int i = 0; i < len; i++)
            {
                int zeros = 0;
                int ones = 0;
                foreach (var line in majLines)
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
                if (majLines.Count > 1)
                {
                    var c = zeros > ones ? '0' : '1';
                    var newLines = new List<string>();
                    foreach (var line in majLines)
                    {
                        if (line[i] == c)
                        {
                            newLines.Add(line);
                        }
                    }
                    majLines = newLines;
                }
            }
            var minLines = lines;
            for (int i = 0; i < len; i++)
            {
                int zeros = 0;
                int ones = 0;
                foreach (var line in minLines)
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
                if (minLines.Count > 1)
                {
                    var c = zeros <= ones ? '0' : '1';
                    var newLines = new List<string>();
                    foreach (var line in minLines)
                    {
                        if (line[i] == c)
                        {
                            newLines.Add(line);
                        }
                    }
                    minLines = newLines;
                }
            }
            Console.WriteLine(Convert.ToInt64(majLines[0], 2) * Convert.ToInt64(minLines[0], 2));
        }
    }
}
