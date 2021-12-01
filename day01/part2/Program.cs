using System;
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
                string line;
                int[] numbers = new int[3]{ -1, -1, -1};
                int i = 0;
                int previousSum = -1, currentSum;
                while ((line = reader.ReadLine()) != null) {
                    int j = (i+1)%numbers.Length; 
                    int currentSum = numbers.Sum();
                    numbers[j] = int.Parse(line);
                    if (numbers[i]!=-1 && numbers[j]>numbers[i]) {
                        result += 1;
                    }
                    i = j;
                }
            }
            Console.WriteLine(result);
        }
    }
}
