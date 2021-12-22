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
            var positions = new int[] { 0, 0 };
            var scores = new int[] { 0, 0 };
            foreach (var line in lines)
            {
                var words = line.Split(" ");
                var player = int.Parse(words[1]) - 1;
                var position = int.Parse(words[4]) - 1;
                var score = 0;
                positions[player] = position;
                scores[player] = score;
            }
            var wins = Play(0, positions, scores);
            var highest = long.MinValue;
            foreach (var score in wins)
            {
                highest = Math.Max(highest, score);
            }
            Console.WriteLine(highest);
        }

        static long[] Play(int player, int[] positions, int[] scores)
        {
            if (scores[0] >= 21)
            {
                return new long[] { 1, 0 };
            }
            if (scores[1] >= 21)
            {
                return new long[] { 0, 1 };
            }

            var nextPlayer = player == 0 ? 1 : 0;
            long[] wins = { 0, 0 };
            int[] frequencies = { 1, 3, 6, 7, 6, 3, 1 };
            for (var rollTotal = 3; rollTotal <= 9; rollTotal++)
            {
                if (player == 0)
                {
                    int[] newPositions = { (positions[0] + rollTotal) % 10, positions[1] };
                    int[] newScore = { scores[0] + newPositions[0] + 1, scores[1] };
                    var newWins = Play(nextPlayer, newPositions, newScore);
                    wins[0] += frequencies[rollTotal - 3] * newWins[0];
                    wins[1] += frequencies[rollTotal - 3] * newWins[1];
                }
                else
                {
                    int[] newPositions = { positions[0], (positions[1] + rollTotal) % 10 };
                    int[] newScore = { scores[0], scores[1] + newPositions[1] + 1, };
                    var newWins = Play(nextPlayer, newPositions, newScore);
                    wins[0] += frequencies[rollTotal - 3] * newWins[0];
                    wins[1] += frequencies[rollTotal - 3] * newWins[1];
                }
            }
            return wins;
        }
    }
}
