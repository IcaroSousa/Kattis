using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kattis.AreaFromPoints
{
    public class Hull
    {

        public Hull(Point[] pPoints)
        {
            InputPoints = new List<Point>(pPoints);
            HullPoints = new List<Point>();

            QuickHull();
        }

        public List<Point> HullPoints;
        public List<Point> InputPoints;
        public double Area => CalcArea();

        private double CalcArea()
        {
            double _Area = 0.0;
            HullPoints.Insert(HullPoints.Count, HullPoints[0]);

            for (int _Index = 0; _Index < HullPoints.Count - 1; _Index++)
            {
                _Area += (HullPoints[_Index].X * HullPoints[_Index + 1].Y) - (HullPoints[_Index + 1].X * HullPoints[_Index].Y);
            }

            return Math.Abs(_Area / 2);
        }

        private bool ClockSide(Point pPointA, Point pPointB, Point pPointC)
        {
            return (pPointC.Y - pPointA.Y) * (pPointB.X - pPointA.X) - (pPointB.Y - pPointA.Y) * (pPointC.X - pPointA.X) > 0;             
        }

        private int GetDistance(Point pPointA, Point pPointB, Point pPonintC)
        {
            return Math.Abs((pPonintC.Y - pPointA.Y) * (pPointB.X - pPointA.X) - (pPointB.Y - pPointA.Y) * (pPonintC.X - pPointA.X));
        }

        private void CreateHull(Point pPointA, Point pPointB, Point[] pPoints)
        {
            if (pPoints == null) return;

            int _PositionToInsert = HullPoints.IndexOf(pPointB);
            if (pPoints.Length == 1)
            {
                HullPoints.Insert(_PositionToInsert, pPoints[0]);
                return;
            }

            int _DistanceToLine = int.MinValue;
            int MostDistantPOintIndex = 0;

            for (int _Index = 0; _Index < pPoints.Length; _Index++)
            {
                int _Distance = GetDistance(pPointA, pPointB, pPoints[_Index]);
                if (_Distance > _DistanceToLine)
                {
                    _DistanceToLine = _Distance;
                    MostDistantPOintIndex = _Index;
                }
            }

            Point _Point = pPoints[MostDistantPOintIndex];
            HullPoints.Insert(_PositionToInsert, _Point);

            Point[] LeftPoints = null;
            Point[] RightPoints = null;

            for (int _Index = 0; _Index < pPoints.Length; _Index++)
            {
                if (ClockSide(pPointA, _Point, pPoints[_Index]))
                {
                    if (LeftPoints == null)
                    {
                        LeftPoints = new Point[] { _Point };
                    }
                    else
                    {
                        Array.Resize(ref LeftPoints, LeftPoints.Length + 1);
                        LeftPoints[LeftPoints.Length - 1] = _Point;   
                    }
                }

                if (ClockSide(_Point, pPointB, pPoints[_Index]))
                {
                    if (RightPoints == null)
                    {
                        RightPoints = new Point[] { _Point };
                    }
                    else
                    {
                        Array.Resize(ref RightPoints, RightPoints.Length + 1);
                        RightPoints[RightPoints.Length - 1] = _Point;
                    }
                }
            }

            CreateHull(pPointA, _Point, LeftPoints);
            CreateHull(_Point, pPointB, RightPoints);
        }

        void QuickHull()
        {

            if (InputPoints.Count <= 3)
            {
                for (int i = 0, InputPointsCount = InputPoints.Count; i < InputPointsCount; i++)
                {                    
                    HullPoints.Add(InputPoints[i]);
                }

                return;
            }

            Point _SmallestPoint = InputPoints.FirstOrDefault(pX => pX.X == InputPoints.Min(pMin => pMin.X));
            Point _BiggerPoint = InputPoints.FirstOrDefault(pX => pX.X == InputPoints.Max(pMax => pMax.X));

            HullPoints.Add(_SmallestPoint);
            HullPoints.Add(_BiggerPoint);

            Point[] left = null;
            Point[] right = null;

            for (int i = 0, InputPointsCount = InputPoints.Count; i < InputPointsCount; i++)
            {
                Point _Point = InputPoints[i];
                if (ClockSide(_SmallestPoint, _BiggerPoint, _Point))
                {
                    if (left == null) 
                    {
                        left = new Point[] { _Point };
                    }
                    else
                    {
                        Array.Resize(ref left, left.Length + 1);
                        left[left.Length - 1] = _Point;   
                    }
                }
                else if (!ClockSide(_SmallestPoint, _BiggerPoint, _Point))
                {
                    if (right == null)
                    {
                        right = new Point[] { _Point };
                    }
                    else
                    {
                        Array.Resize(ref right, right.Length + 1);
                        right[right.Length - 1] = _Point;   
                    }
                }
            }

            CreateHull(_SmallestPoint, _BiggerPoint, left);
            CreateHull(_BiggerPoint, _SmallestPoint, right);
        }
    }

    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int pX, int pY)
        {
            X = pX;
            Y = pY;
        }
    }

    public static class AreaFromPoint
    {
        public static void Main()
        {
            string _Input = Console.ReadLine();
            while (!_Input.Equals("0"))
            {
                Stopwatch _Watch = Stopwatch.StartNew();

                int _Loop = int.Parse(_Input);
                Point[] _Points = new Point[_Loop];

                for (int _Index = 0; _Index < _Loop; _Index++)
                {                    
                    var _point = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None).ToArray();
                    _Points[_Index] = new Point(int.Parse(_point[0]), int.Parse(_point[1]));                                       
                }

                Hull _Hull = new Hull(_Points);
                Console.WriteLine(_Hull.Area.ToString("F1"));

                _Watch.Stop();
                Console.Write($"Execution Time : {_Watch.ElapsedMilliseconds}");

                _Input = Console.ReadLine();
            }
        }
    }
}
