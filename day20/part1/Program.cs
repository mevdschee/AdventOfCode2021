﻿using System;
using System.IO;
using System.Collections;
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
            var lookup = new Dictionary<int, bool>();
            var field = new Dictionary<(int, int), bool>();
            var width = lines[2].Length;
            var height = lines.Count - 2;
            var py = 0;
            foreach (var line in lines)
            {
                if (lookup.Count == 0)
                {
                    for (var i = 0; i < line.Length; i++)
                    {
                        lookup[i] = line[i] == '#';
                    }
                    continue;
                }
                if (line == "")
                {
                    continue;
                }
                for (var x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#')
                    {
                        field[(x, py)] = true;
                    }
                    else
                    {
                        field[(x, py)] = false;
                    }
                }
                py += 1;
            }
            for (var step = 1; step <= 2; step++)
            {
                var newField = new Dictionary<(int, int), bool>();
                for (var y = 0 - step; y < height + step; y++)
                {
                    for (var x = 0 - step; x < width + step; x++)
                    {
                        var bin = "";
                        for (var dy = -1; dy <= 1; dy++)
                        {
                            for (var dx = -1; dx <= 1; dx++)
                            {
                                if (!field.ContainsKey((x + dx, y + dy)))
                                {
                                    field[(x + dx, y + dy)] = (lookup[0] && (step % 2 == 0));
                                }
                                bin += field[(x + dx, y + dy)] ? '1' : '0';
                            }
                        }
                        var index = Convert.ToInt32(bin, 2);
                        newField[(x, y)] = lookup[index];
                    }
                }
                field = newField;
                printField(field, width, height, step);
            }
            var result = 0;
            foreach (var b in field.Values)
            {
                result += b ? 1 : 0;
            }
            Console.WriteLine(result);
            // input.test1 = 5326
        }

        static void printField(Dictionary<(int, int), bool> field, int width, int height, int step)
        {
            for (var y = 0 - step; y < height + step; y++)
            {
                var line = "";
                for (var x = 0 - step; x < width + step; x++)
                {
                    line += field[(x, y)] ? '#' : '.';
                }
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
    }
}