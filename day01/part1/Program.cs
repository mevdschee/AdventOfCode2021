using System;
using System.IO;

namespace part1
{
    class Program
    {
        static void Main(string[] args)
        {
            int result = 0;
            using (StreamReader reader = new StreamReader("input"))
            {
                string line;
                int previousNumber=-1,currentNumber;
                while ((line = reader.ReadLine()) != null) {
                    currentNumber = int.Parse(line);
                    if (previousNumber!=-1 && currentNumber>previousNumber) {
                        result += 1;
                    }
                    previousNumber = currentNumber;
                }
            }
            Console.WriteLine(result);
        }
    }
}
