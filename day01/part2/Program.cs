using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace part2
{
    class Program
    {
        static void Main(string[] args)
        {
            int result = 0;
            using (StreamReader reader = new StreamReader("input"))
            {
                var numbers = new List<int>();
                string line;
                while ((line = reader.ReadLine()) != null) {
                    int previousSum = numbers.Sum();
                    numbers.Add(int.Parse(line));
                    if (numbers.Count>3) {
                        numbers.RemoveAt(0);
                        int currentSum = numbers.Sum();
                        if (currentSum>previousSum) {
                            result += 1;
                        }
                    }
                }
            }
            Console.WriteLine(result);
        }
    }
}
