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
            var missing = 0;
            var parts = new Dictionary<string, Dictionary<string, int>>();
            foreach (var line in lines)
            {
                if (missing == 0) 
                {
                    missing = int.Parse(line.Split(" ")[0]);
                    continue;
                }
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
            var partCounts = new Dictionary<string,int>();
            foreach (var part in toys.Keys)
            {  
                var count = 0;
                foreach (var subcount in parts[part].Values)
                {
                    count += subcount;
                }
                partCounts.Add(part,count);
            }
            Console.WriteLine(BFS("",0,0,missing,20,partCounts));
        }

        private static string BFS(string path, int depth, int count, int missing, int presents, Dictionary<string,int> partCounts)
        {
            if (depth>presents) {
                return "";
            }
            if (count==missing) {
                if (depth==presents) 
                {
                    return path;
                }
                return "";
            }
            if (count>missing) {
                return "";
            }
            var result = "";
            foreach (var part in partCounts.Keys)
            {
                if (path.Length>0 && part[0]<path[path.Length-1])
                {
                    continue;
                }
                result = BFS(path+part[0],depth+1,count+partCounts[part],missing,presents,partCounts);
                if (result.Length>0)
                {
                    break;
                }
            }
            return result;
        }

    }
}
