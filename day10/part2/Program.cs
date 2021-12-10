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
                {')', 1},
                {']', 2},
                {'}', 3},
                {'>', 4}
            };
            var scores = new List<long>();
            foreach (var line in lines)
            {
                var stack = new Stack<char>();
                var corrupted = false;
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
                            corrupted = true;
                            break;
                        }
                    }
                }
                if (!corrupted)
                {
                    long score = 0;
                    foreach (var c in stack)
                    {
                        score = score * 5 + points[c];
                    }
                    scores.Add(score);
                }
            }
            scores.Sort();
            scores.Reverse();
            Console.WriteLine(scores[scores.Count/2]);
        }
    }
}
