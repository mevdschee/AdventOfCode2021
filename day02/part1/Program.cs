using System;
using System.IO;

namespace part1
{
    class Program
    {
        static void Main(string[] args)
        {
            int position = 0, depth = 0;
            using (StreamReader reader = new StreamReader("input"))
            {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    var parts = line.Split(' ');
                    var distance = int.Parse(parts[1]);
                    switch(parts[0]){
                        case "forward":
                            position += distance;
                            break;
                        case "down":
                            depth += distance;
                            break;
                        case "up":
                            depth -= distance;
                            break;
                    } 
                }
            }
            Console.WriteLine(position*depth);
        }
    }
}
