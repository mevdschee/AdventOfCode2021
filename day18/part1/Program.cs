using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Program
{
    class Program
    {
        class RefInt
        {
            public int number;
            public RefInt(int number)
            {
                this.number = number;
            }

            override public string ToString()
            {
                return number.ToString();
            }
        }

        class Pair
        {
            public RefInt Left = null;

            public RefInt Right = null;

            public Pair LeftPair = null;

            public Pair RightPair = null;

            public Pair Parent = null;

            public static Pair Read(StringReader reader)
            {
                var pair = new Pair();
                if (reader.Read() != '[')
                {
                    throw new Exception("expected [");
                }
                if (reader.Peek() == '[')
                {
                    pair.LeftPair = Pair.Read(reader);
                    pair.LeftPair.Parent = pair;
                }
                else
                {
                    pair.Left = new RefInt(int.Parse(new string((char)reader.Read(), 1)));
                }
                if (reader.Read() != ',')
                {
                    throw new Exception("expected ,");
                }
                if (reader.Peek() == '[')
                {
                    pair.RightPair = Pair.Read(reader);
                    pair.RightPair.Parent = pair;
                }
                else
                {
                    pair.Right = new RefInt(int.Parse(new string((char)reader.Read(), 1)));
                }
                if (reader.Read() != ']')
                {
                    throw new Exception("expected ]");
                }
                return pair;
            }

            override public string ToString()
            {
                string s = "[";
                if (LeftPair != null)
                {
                    s += LeftPair.ToString();
                }
                else
                {
                    s += Left.ToString();
                }
                s += ",";
                if (RightPair != null)
                {
                    s += RightPair.ToString();
                }
                else
                {
                    s += Right.ToString();
                }
                s += "]";
                return s;
            }

            public bool Explode(ref (Pair exploded, RefInt lastLeft, RefInt lastRight) state, int depth = 0)
            {
                if (LeftPair != null)
                {
                    LeftPair.Explode(ref state, depth + 1);
                }
                else
                {
                    if (state.exploded == null && depth >= 4)
                    {
                        state.exploded = this;
                        return true;
                    }
                    else
                    {
                        if (state.exploded == null)
                        {
                            state.lastLeft = Left;
                        }
                        else if (state.lastRight == null)
                        {
                            state.lastRight = Left;
                        }
                    }
                }
                if (RightPair != null)
                {
                    RightPair.Explode(ref state, depth + 1);
                }
                else
                {
                    if (state.exploded == null)
                    {
                        state.lastLeft = Right;
                    }
                    else if (state.lastRight == null)
                    {
                        state.lastRight = Right;
                    }
                }
                if (depth == 0)
                {
                    if (state.exploded != null)
                    {
                        if (state.lastLeft != null)
                        {
                            state.lastLeft.number += state.exploded.Left.number;
                        }
                        if (state.lastRight != null)
                        {
                            state.lastRight.number += state.exploded.Right.number;
                        }
                        if (state.exploded.Parent.LeftPair == state.exploded)
                        {
                            state.exploded.Parent.LeftPair = null;
                            state.exploded.Parent.Left = new RefInt(0);
                        }
                        else
                        {
                            state.exploded.Parent.RightPair = null;
                            state.exploded.Parent.Right = new RefInt(0);
                        }
                        return true;
                    }
                }
                return false;
            }

            public bool Split(ref (Pair split, bool isLeft) state, int depth = 0)
            {
                if (LeftPair != null)
                {
                    LeftPair.Split(ref state, depth + 1);
                }
                else
                {
                    if (Left.number > 9)
                    {
                        if (state.split == null)
                        {
                            state.split = this;
                            state.isLeft = true;
                        }
                    }
                }
                if (RightPair != null)
                {
                    RightPair.Split(ref state, depth + 1);
                }
                else
                {
                    if (Right.number > 9)
                    {
                        if (state.split == null)
                        {
                            state.split = this;
                            state.isLeft = false;
                        }
                    }
                }
                if (depth == 0)
                {
                    if (state.split != null)
                    {
                        if (state.isLeft)
                        {
                            var leftNumber = state.split.Left.number / 2;
                            var rightNumber = state.split.Left.number - leftNumber;
                            state.split.Left = null;
                            var pair = new Pair();
                            pair.Left = new RefInt(leftNumber);
                            pair.Right = new RefInt(rightNumber);
                            pair.Parent = state.split;
                            state.split.LeftPair = pair;
                        }
                        else
                        {
                            var leftNumber = state.split.Right.number / 2;
                            var rightNumber = state.split.Right.number - leftNumber;
                            state.split.Right = null;
                            var pair = new Pair();
                            pair.Left = new RefInt(leftNumber);
                            pair.Right = new RefInt(rightNumber);
                            pair.Parent = state.split;
                            state.split.RightPair = pair;
                        }
                        return true;
                    }
                }
                return false;
            }

            public Pair Add(Pair right)
            {
                var pair = new Pair();
                pair.LeftPair = this;
                pair.RightPair = right;
                this.Parent = pair;
                right.Parent = pair;
                return pair;
            }

            public int Magnitude()
            {
                var result = 0;
                if (LeftPair != null)
                {
                    result += 3 * LeftPair.Magnitude();
                }
                else
                {
                    result += 3 * Left.number;
                }
                if (RightPair != null)
                {
                    result += 2 * RightPair.Magnitude();
                }
                else
                {
                    result += 2 * Right.number;
                }
                return result;
            }

            public void Reduce()
            {
                var changed = false;
                do
                {
                    changed = false;
                    if (!changed)
                    {
                        (Pair, RefInt, RefInt) state = (null, null, null);
                        changed = this.Explode(ref state);
                    }
                    if (!changed)
                    {
                        (Pair, bool) state = (null, false);
                        changed = this.Split(ref state);
                    }
                }
                while (changed);
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
            Pair pair = null;
            foreach (var line in lines)
            {
                if (pair == null)
                {
                    pair = Pair.Read(new StringReader(line));
                    continue;
                }
                //Console.WriteLine(pair.ToString());
                pair = pair.Add(Pair.Read(new StringReader(line)));
                pair.Reduce();
            }
            //Console.WriteLine(pair.ToString());
            Console.WriteLine(pair.Magnitude());
        }

    }
}
