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
                    if (line.Length > 0)
                    {
                        lines.Add(line);
                    }
                }
            }
            var points = new Dictionary<char, int>
            {
                {')', 3},
                {']', 57},
                {'}', 1197},
                {'>', 25137}
            };
            var result = 0;
            foreach (var line in lines)
            {
                var stack = new Stack<char>();
                foreach (var c in line)
                {
                    var chars = "([{<)]}>";
                    var p = chars.IndexOf(c);
                    if (p < 4)
                    {
                        stack.Push(chars[p + 4]);
                    }
                    else
                    {
                        var expected = stack.Pop();
                        if (c != expected)
                        {
                            result += points[c];
                        }
                    }
                }
            }
            Console.WriteLine(result);
        }
    }
}
