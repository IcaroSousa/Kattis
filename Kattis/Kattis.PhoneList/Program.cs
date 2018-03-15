using System;

namespace Kattis.PhoneList
{
    static class PhoneList
    {
        static void Main(string[] args)
        {
            int _Cases = int.Parse(Console.ReadLine());

            while (_Cases > 0)
            {
                //var _Watch = System.Diagnostics.Stopwatch.StartNew();
                bool _Findded = false;

                string[] _PhoneList = new string[int.Parse(Console.ReadLine())];

                for (int _InputIndex = 0; _InputIndex < _PhoneList.Length; _InputIndex++)
                {
                    _PhoneList[_InputIndex] = Console.ReadLine();
                }

                Array.Sort(_PhoneList);

                for (int _Index = 0, _PhoneListLength = _PhoneList.Length - 1; _Index < _PhoneListLength; _Index++)
                {
                    string _Phone = _PhoneList[_Index + 1];
                    string _Find = _PhoneList[_Index];

                    _Findded = (_Phone.Length > _Find.Length) && (_Find == _Phone.Substring(0, _Find.Length));
                    if (_Findded) break;
                }

                Console.WriteLine(_Findded ? "NO" : "YES");

                //_Watch.Stop();
                //Console.Write($"Execution Time : {_Watch.ElapsedMilliseconds}");

                _Cases--;
            }
        }
    }
}