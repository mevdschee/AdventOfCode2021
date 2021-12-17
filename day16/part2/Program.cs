using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Program
{
    class Program
    {
        struct Packet
        {
            public int Version;
            public int Type;
            public long Literal;
            public List<Packet> SubPackets;

            public int GetVersionSum()
            {
                int sum = Version;
                foreach (var packet in SubPackets)
                {
                    sum += packet.GetVersionSum();
                }
                return sum;
            }

            public long Evaluate()
            {
                switch (Type)
                {
                    case 0:
                        return Sum();
                    case 1:
                        return Product();
                    case 2:
                        return Minumum();
                    case 3:
                        return Maximum();
                    case 4:
                        return Literal;
                    case 5:
                        return GreaterThan();
                    case 6:
                        return LessThan();
                    case 7:
                        return EqualTo();
                    default:
                        throw new NotImplementedException();
                }
            }

            public long Sum()
            {
                long sum = 0;
                foreach (var packet in SubPackets)
                {
                    sum += packet.Evaluate();
                }
                return sum;
            }

            public long Product()
            {
                long product = 1;
                foreach (var packet in SubPackets)
                {
                    product *= packet.Evaluate();
                }
                return product;
            }

            public long Minumum()
            {
                var minimum = long.MaxValue;
                foreach (var packet in SubPackets)
                {
                    minimum = Math.Min(minimum, packet.Evaluate());
                }
                return minimum;
            }

            public long Maximum()
            {
                long maximum = long.MinValue;
                foreach (var packet in SubPackets)
                {
                    maximum = Math.Max(maximum, packet.Evaluate());
                }
                return maximum;
            }

            public long GreaterThan()
            {
                return SubPackets[0].Evaluate() > SubPackets[1].Evaluate() ? 1 : 0;
            }

            public long LessThan()
            {
                return SubPackets[0].Evaluate() < SubPackets[1].Evaluate() ? 1 : 0;
            }

            public long EqualTo()
            {
                return SubPackets[0].Evaluate() == SubPackets[1].Evaluate() ? 1 : 0;
            }
        }

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
            var packet = ParsePacket(bytes);
            Console.WriteLine(packet.Evaluate());
        }

        static Packet ParsePacket(byte[] bytes)
        {
            var packets = new List<Packet>();
            ParsePackets(bytes, 0, bytes.Length * 8, 1, packets);
            return packets[0];
        }

        static int ParsePackets(byte[] bytes, int bitpos, int bitlen, int packets, List<Packet> subpackets)
        {
            var p = bitpos;
            while (p < bitpos + bitlen)
            {
                var packet = new Packet();
                //Console.WriteLine("packet p={0}", p);
                packet.Version = Read(bytes, p, 3); p += 3;
                packet.Type = Read(bytes, p, 3); p += 3;
                //Console.WriteLine("type {0} p={1}", packet.Type, p - 3);
                packet.Literal = 0;
                packet.SubPackets = new List<Packet>();
                if (packet.Type == 4)
                {
                    var nibbles = new List<long>();
                    do
                    {
                        var nibble = Read(bytes, p + 1, 4); p += 5;
                        //Console.WriteLine("-nibble {0}", nibble);
                        packet.Literal = nibble | (packet.Literal << 4);
                        //Console.WriteLine("-literal {0} p={1}", packet.Literal, p - 5);
                        nibbles.Add(nibble);
                    }
                    while (Read(bytes, p - 5, 1) == 1);
                    //Console.WriteLine("literal {0}", packet.Literal);
                }
                else
                {
                    var lenlen = (Read(bytes, p, 1) == 1) ? 11 : 15; p += 1;
                    var length = Read(bytes, p, lenlen); p += lenlen;
                    if (lenlen == 11)
                    {
                        //Console.WriteLine("packet length {0}", length);
                        p += ParsePackets(bytes, p, bitlen, length, packet.SubPackets);
                    }
                    else
                    {
                        //Console.WriteLine("bit length {0}", length);
                        p += ParsePackets(bytes, p, length, -1, packet.SubPackets);
                    }
                }
                subpackets.Add(packet);
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
