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
            using (StreamReader reader = new StreamReader("input.test1"))
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
            var bytes = Convert.FromHexString(lines[0]);
            for (var p = 0; p < bytes.Length; p++)
            {
                for (var b = 0; b < 8; b++)
                {
                    Console.Write((bytes[p] & (1 << (7 - b))) > 0 ? '1' : '0');
                }
            }
            Console.WriteLine();
        }
    }
}
