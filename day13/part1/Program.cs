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
            var height = 0;
            var width = 0;
            var field = new Dictionary<(int, int), bool>();
            var folds = new List<(char, int)>();
            var dots = true;
            foreach (var line in lines)
            {
                if (line == "") {
                    dots = false;
                    continue;
                }
                if (dots) {
                    var words = line.Split(',');
                    var x = int.Parse(words[0]);
                    var y = int.Parse(words[1]);
                    width = Math.Max(width,x);
                    height = Math.Max(height,y);
                    field[(x,y)] = true;
                } else {
                    var words = line.Split('=');
                    var c = words[0][words[0].Length-1];
                    var pos = int.Parse(words[1]);
                    folds.Add(c,pos);
                }
            }
            foreach (var (axis,pos) in folds)
            {
                // execute fold
            }
            Console.WriteLine(field.Count);
        }
    }
}
