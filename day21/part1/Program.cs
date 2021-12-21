using System;
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
                    if (line.Length > 0)
                    {
                        lines.Add(line);
                    }
                }
            }
            var positions = new Dictionary<int, int>();
            var scores = new Dictionary<int, int>();
            foreach (var line in lines)
            {
                var words = line.Split(" ");
                var player = int.Parse(words[1]);
                var position = int.Parse(words[4]);
                var score = 0;
                positions[player] = position;
                scores[player] = score;
            }
            var rolls = 0;
            var won = false;
            while (!won)
            {
                foreach (var player in positions.Keys)
                {
                    for (var i = 0; i < 3; i++)
                    {
                        var roll = rolls + 1;
                        positions[player] += roll;
                        rolls += 1;
                    }
                    positions[player] = 1 + (positions[player] - 1) % 10;
                    scores[player] += positions[player];
                    if (scores[player] >= 1000)
                    {
                        won = true;
                        break;
                    }

                }
            }
            var lowestScore = int.MaxValue;
            foreach (var score in scores.Values)
            {
                lowestScore = Math.Min(lowestScore, score);
            }
            Console.WriteLine(rolls * lowestScore);
        }
    }
}
