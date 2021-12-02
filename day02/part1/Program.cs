using System;
using System.IO;

namespace part1
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0, y = 0;
            using (StreamReader reader = new StreamReader("input"))
            {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    var parts = line.Split(' ');
                    var distance = int.Parse(parts[1]);
                    switch(parts[0]){
                        case "forward":
                            x += distance;
                            break;
                        case "down":
                            y -= distance;
                            break;
                        case "up":
                            y += distance;
                            break;
                    } 
                }
            }
            Console.WriteLine(x*(-1*y));
        }
    }
}
