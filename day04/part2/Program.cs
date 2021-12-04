using System;
using System.IO;
using System.Text.RegularExpressions;
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
            var drawn = new List<int>();
            foreach (var num in lines[0].Split(','))
            {
                drawn.Add(int.Parse(num));
            }
            var boards = new List<List<int>>();
            for (var i = 2; i < lines.Count; i += 6)
            {
                var board = new List<int>();
                for (var j = 0; j < 5; j++)
                {
                    var line = lines[i + j].Trim().Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
                    foreach (var num in line)
                    {
                        board.Add(int.Parse(num));
                    }
                }
                boards.Add(board);
            }
            var result = 0;
            foreach (var draw in drawn)
            {
                foreach (var board in boards)
                {
                    for (var i = 0; i < board.Count; i++)
                    {
                        var x = i % 5;
                        var y = i / 5;
                        // strike number
                        if (board[y * 5 + x] == draw)
                        {
                            board[y * 5 + x] = 0;
                        }
                        // check horizontal
                        var hscore = 0;
                        for (var sx = 0; sx < 5; sx++)
                        {
                            if (board[y * 5 + sx] == 0)
                            {
                                hscore += 1;
                            }
                        }
                        // check vertical
                        var vscore = 0;
                        for (var sy = 0; sy < 5; sy++)
                        {
                            if (board[sy * 5 + x] == 0)
                            {
                                vscore += 1;
                            }
                        }
                        // match scores
                        if (hscore == 5 || vscore == 5)
                        {
                            var sum = 0;
                            foreach (var num in board)
                            {
                                sum += num;
                            }
                            result = sum * draw;
                            board.RemoveRange(0, 5*5);
                            break;
                        }
                    }
                }
            }
            Console.WriteLine(result);
        }
    }
}
