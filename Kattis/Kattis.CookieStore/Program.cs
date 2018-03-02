using System;
using System.Collections.Generic;
using System.Linq;

namespace Kattis.CookieStore
{
    class ReverseSorting : IComparer<int>
    {
        public int Compare(int pA, int pB)
        {
            int _result = Comparer<int>.Default.Compare(pA, pB);
            return 0 - _result;
        }
    }

    public static class CookieStore
    {
        public static int GetNumber(this SortedSet<int> pList)
        {
            int _Number = pList.First();
            pList.Remove(_Number);
            return _Number;
        }
        public static void Main(string[] args)
        {
            SortedSet<int> _BigOnes = new SortedSet<int>(new ReverseSorting());
            SortedSet<int> _SmallOnes = new SortedSet<int>();

            _BigOnes.Add(-1);
            _SmallOnes.Add(300000001);

            string _Input;
            while ((_Input = Console.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(_Input)) { break; }

                if (_Input.Equals("#"))
                {
                    if ((_SmallOnes.Count() > 0) || (_BigOnes.Count() > 0))
                    {
                        Console.WriteLine(_SmallOnes.GetNumber());
                        if (_SmallOnes.Count != _BigOnes.Count())
                        {
                            _SmallOnes.Add(_BigOnes.GetNumber());
                        }
                    }
                }
                else
                {
                    int _Number = int.Parse(_Input);
                    if (_Number > _SmallOnes.First())
                    {
                        _SmallOnes.Add(_Number);
                        if (_SmallOnes.Count() > _BigOnes.Count() + 1)
                        {
                            _BigOnes.Add(_SmallOnes.GetNumber());
                        }
                    }
                    else
                    {
                        _BigOnes.Add(_Number);
                        if (_BigOnes.Count() > _SmallOnes.Count())
                        {
                            _SmallOnes.Add(_BigOnes.GetNumber());
                        }
                    }
                }
            }
        }
    }
}
