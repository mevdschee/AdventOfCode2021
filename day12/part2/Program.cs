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
            var vectors = new Dictionary<(string, string), bool>();
            var visits = new Dictionary<string, int>();
            foreach (var line in lines)
            {
                var words = line.Split('-');
                vectors.Add((words[0], words[1]), true);
                vectors.Add((words[1], words[0]), true);
                if (words[0].ToUpper() != words[0])
                {
                    visits[words[0]] = 0;
                }
                if (words[1].ToUpper() != words[1])
                {
                    visits[words[1]] = 0;
                }
            }
            var path = new List<string>();
            path.Add("start");
            visits["start"] = 2;
            var maxVisits = 2;
            Console.WriteLine(Search(path, visits, maxVisits, vectors).Count);
        }

        private static Dictionary<List<string>, bool> Search(List<string> path, Dictionary<string, int> visits, int maxVisits, Dictionary<(string, string), bool> vectors)
        {
            var resultPaths = new Dictionary<List<string>, bool>();
            var last = path[path.Count - 1];
            if (last == "end")
            {
                //Console.WriteLine("{0}",String.Join(", ",path));
                resultPaths.Add(path, true);
                return resultPaths;
            }
            foreach (var (from, to) in vectors.Keys)
            {
                if (from != last)
                {
                    continue;
                }
                if (visits.GetValueOrDefault(to,0) >= maxVisits)
                {
                    continue;
                }
                var newPath = new List<string>(path);
                newPath.Add(to);
                var newVisits = visits;
                var newMaxVisits = maxVisits;
                if (newVisits.ContainsKey(to))
                { 
                    newVisits = new Dictionary<string, int>(visits);
                    newVisits[to] += 1;
                    if (newVisits[to] == 2)
                    {
                        newMaxVisits = 1;
                    }
                }
                var foundPaths = Search(newPath, newVisits, newMaxVisits, vectors);
                foreach (var foundPath in foundPaths.Keys)
                {
                    resultPaths[foundPath] = true;
                }
            }
            return resultPaths;
        }

    }
}
