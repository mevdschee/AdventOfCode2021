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
                reader.ReadLine();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length > 0)
                    {
                        lines.Add(line);
                    }
                }
            }
            var parts = new Dictionary<string, Dictionary<string, int>>();
            foreach (var line in lines)
            {
                var part = line.Split(": ");
                var subparts = new Dictionary<string, int>();
                foreach (var subpart in part[1].Split(", ")) 
                {
                    var words = subpart.Split(" ");
                    subparts.Add(words[1],int.Parse(words[0]));
                }
                parts.Add(part[0], subparts);
            }
            var toys = new Dictionary<string, bool>();
            foreach (var part in parts.Keys)
            {
                toys[part] = true;
            }
            foreach (var subparts in parts.Values)
            {
                foreach (var subpart in subparts.Keys)
                {
                    if (toys.ContainsKey(subpart))
                    {
                        toys.Remove(subpart);
                    }
                }
            }
            var reduced = true;
            while (reduced)
            {
                reduced = false;
                foreach (var part in parts.Keys)
                {
                    var remove = new List<string>();
                    var add = new List<(string,int)>();
                    foreach (var subpart in parts[part].Keys)
                    {
                        var subcount = parts[part][subpart];
                        if (parts.ContainsKey(subpart))
                        {
                            reduced = true;
                            remove.Add(subpart);
                            foreach (var subsubpart in parts[subpart].Keys)
                            {
                                var subsubcount = parts[subpart][subsubpart];
                                add.Add((subsubpart, subcount * subsubcount));
                            }
                        }
                    }
                    foreach (var subpart in remove)
                    {
                        parts[part].Remove(subpart);
                    }
                    foreach (var (subpart,subcount) in add)
                    {
                        var count = parts[part].GetValueOrDefault(subpart,0);
                        parts[part][subpart] = count + subcount;
                    }
                }
            }
            var result = 0;
            foreach (var part in toys.Keys)
            {  
                var count = 0;
                foreach (var subcount in parts[part].Values)
                {
                    count += subcount;
                }
                result = Math.Max(result, count);
            }
            Console.WriteLine(result);
        }
    }
}
