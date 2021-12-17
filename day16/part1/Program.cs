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
            var bytes = Convert.FromHexString(lines[0]);
            //for (var by = 0; by < bytes.Length; by++)
            //{
            //    for (var bi = 0; bi < 8; bi++)
            //    {
            //        Console.Write((bytes[by] & (1 << (7 - bi))) > 0 ? '1' : '0');
            //    }
            //}
            //Console.WriteLine();
            var versions = new List<int>();
            SumVersion(bytes, 0, bytes.Length * 8, 1, versions);
            var sum = 0;
            foreach (var version in versions)
            {
                sum += version;
            }
            Console.WriteLine(sum);
        }

        static int SumVersion(byte[] bytes, int bitpos, int bitlen, int packets, List<int> versions)
        {
            var p = bitpos;
            while (p < bitpos + bitlen)
            {
                //Console.WriteLine("packet p={0}", p);


                var version = Read(bytes, p, 3); p += 3;
                versions.Add(version);
                var type = Read(bytes, p, 3); p += 3;
                //Console.WriteLine("type {0} p={1}", type, p - 3);
                if (type == 4)
                {
                    var literal = 0;
                    var nibbles = new List<int>();
                    do
                    {
                        var nibble = Read(bytes, p + 1, 4); p += 5;
                        //Console.WriteLine("-nibble {0}", nibble);
                        literal = nibble | (literal << 4);
                        //Console.WriteLine("-literal {0} p={1}", literal, p - 5);
                        nibbles.Add(nibble);
                    }
                    while (Read(bytes, p - 5, 1) == 1);
                    //Console.WriteLine("literal {0}", literal);
                }
                else
                {
                    var lenlen = (Read(bytes, p, 1) == 1) ? 11 : 15; p += 1;
                    var length = Read(bytes, p, lenlen); p += lenlen;
                    if (lenlen == 11)
                    {
                        //Console.WriteLine("packet length {0}", length);
                        p += SumVersion(bytes, p, bitlen, length, versions);
                    }
                    else
                    {
                        //Console.WriteLine("bit length {0}", length);
                        p += SumVersion(bytes, p, length, -1, versions);
                    }
                }
                packets -= 1;
                if (packets == 0)
                {
                    break;
                }
            }
            return p - bitpos;
        }

        static int Read(byte[] bytes, int bitpos, int bitlen)
        {
            var bits = new Dictionary<int, (int, int)>();
            for (var i = 0; i < bitlen; i++)
            {
                bits.Add(i, ((bitpos + i) / 8, (bitpos + i) % 8));
            }
            var result = 0;
            foreach (var i in bits.Keys)
            {
                var (by, bi) = bits[i];
                var bit = (bytes[by] & (1 << (7 - bi))) > 0;
                if (bit)
                {
                    result |= 1 << (bitlen - 1 - i);
                }
            }
            return result;
        }
    }
}
