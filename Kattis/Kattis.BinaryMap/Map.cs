using System;
using System.Collections.Generic;
using System.Linq;

namespace Kattis.BinaryMap
{
    public static class Map
    {
        public static int LookAround(List<char[]> pMap, int pPositionA, int pPositionB)
        {
            int _Result = 4;

            if (pMap[pPositionA][pPositionB].Equals('1'))
            {                
                if (pPositionA > 0 && pMap[pPositionA - 1][pPositionB].Equals('1'))
                    _Result--;
                
                if (pPositionA < pMap.Count - 1 && pMap[pPositionA + 1][pPositionB].Equals('1'))
                    _Result--;
                
                if (pPositionB > 0 && pMap[pPositionA][pPositionB - 1].Equals('1'))
                    _Result--;
                
                if (pPositionB < pMap[0].Length && pMap[pPositionA][pPositionB + 1].Equals('1'))
                    _Result--;

            }
            else
            {
                bool InnerIsland = false;

                InnerIsland = (pPositionA > 0 && pMap[pPositionA - 1][pPositionB].Equals('1'));
                InnerIsland = (InnerIsland && (pPositionA < pMap.Count - 1) && pMap[pPositionA + 1][pPositionB].Equals('1'));
                InnerIsland = (InnerIsland && pPositionB > 0 && pMap[pPositionA][pPositionB - 1].Equals('1'));
                InnerIsland = (InnerIsland && pPositionB < pMap[0].Length && pMap[pPositionA][pPositionB + 1].Equals('1'));

                if (InnerIsland) _Result = -4;
            }

            return _Result;
        }

        public static void Main()
        {

            string[] _Input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None).ToArray();
            int _A = int.Parse(_Input[0]);
            int _B = int.Parse(_Input[1]);

            List<char[]> _Map = new List<char[]>();

            for (int _Index = 0; _Index < _A; _Index++)
            {
                _Map.Add(Console.ReadLine().ToCharArray());
            }

            int _Result = 0;
            for (int _IndexA = 0; _IndexA < _A; _IndexA++)
            {
                for (int _IndexB = 0; _IndexB < _B; _IndexB++)
                {
                    _Result += LookAround(_Map, _IndexA, _IndexB);
                }
            }

            Console.WriteLine(_Result);               
        }
    }
}
