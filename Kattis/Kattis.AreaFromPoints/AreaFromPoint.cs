using System;
using System.Collections.Generic;
using System.Linq;

namespace Kattis.AreaFromPoints
{
    public class Hull
    {

        public Hull(List<Point> pPoints)
        {
            InputPoints = new List<Point>();
            HullPoints = new List<Point>();

            InputPoints = pPoints;
            QuickHull();

        }

        public List<Point> HullPoints;
        public List<Point> InputPoints;
        public double Area => CalcArea();

        private double CalcArea()
        {
            double _Area = 0.0;
            HullPoints.Insert(HullPoints.Count(), HullPoints.First());

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

        private void CreateHull(Point pPointA, Point pPointB, List<Point> pPoints)
        {
            int _PositionToInsert = HullPoints.IndexOf(pPointB);

            if (!pPoints.Any())
            {
                return;
            }

            if (pPoints.Count == 1)
            {
                HullPoints.Insert(_PositionToInsert, pPoints.First());
                return;
            }

            int _DistanceToLine = int.MinValue;
            int MostDistantPOintIndex = 0;

            for (int _Index = 0; _Index < pPoints.Count; _Index++)
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

            List<Point> LeftPoints = new List<Point>();
            List<Point> RightPoints = new List<Point>();

            for (int _Index = 0; _Index < pPoints.Count; _Index++)
            {
                if (ClockSide(pPointA, _Point, pPoints[_Index]))
                {
                    LeftPoints.Add(pPoints[_Index]);
                }

                if (ClockSide(_Point, pPointB, pPoints[_Index]))
                {
                    RightPoints.Add(pPoints[_Index]);
                }
            }

            CreateHull(pPointA, _Point, LeftPoints);
            CreateHull(_Point, pPointB, RightPoints);
        }

        private void QuickHull()
        {

            if (InputPoints.Count <= 3)
            {
                for (int i = 0, InputPointsCount = InputPoints.Count; i < InputPointsCount; i++)
                {
                    Point _Point = InputPoints[i];
                    HullPoints.Add(_Point);
                }

                return;
            }

            Point _SmallestPoint = InputPoints.FirstOrDefault(pX => pX.X == InputPoints.Min(pMin => pMin.X));
            Point _BiggerPoint = InputPoints.FirstOrDefault(pX => pX.X == InputPoints.Max(pMax => pMax.X));

            HullPoints.Add(_SmallestPoint);
            HullPoints.Add(_BiggerPoint);

            List<Point> left = new List<Point>();
            List<Point> right = new List<Point>();

            for (int i = 0, InputPointsCount = InputPoints.Count; i < InputPointsCount; i++)
            {
                Point _Point = InputPoints[i];
                if (ClockSide(_SmallestPoint, _BiggerPoint, _Point))
                {
                    left.Add(_Point);
                }
                else if (!ClockSide(_SmallestPoint, _BiggerPoint, _Point))
                {
                    right.Add(_Point);
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
                int _Loop = int.Parse(_Input);
                List<Point> _Points = new List<Point>();

                for (int _Index = 0; _Index < _Loop; _Index++)
                {
                    _Input = Console.ReadLine();
                    string[] _point = _Input.Split(new char[] { ' ' }, StringSplitOptions.None).ToArray();

                    _Points.Add(new Point(int.Parse(_point[0]), int.Parse(_point[1])));
                }


                Hull _Hull = new Hull(_Points);
                Console.WriteLine(_Hull.Area.ToString("F1"));
               

                _Input = Console.ReadLine();
            }
        }
    }
}
