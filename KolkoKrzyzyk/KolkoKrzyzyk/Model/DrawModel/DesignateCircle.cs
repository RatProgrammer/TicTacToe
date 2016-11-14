using System;
using System.Collections.Generic;
using System.Drawing;

namespace TicTacToe.Model.DrawModel
{
    class DesignateCircle
    {
        private readonly List<Point> _points;

        public DesignateCircle()
        {
            _points = new List<Point>();
        }

        public List<Point> Designate()
        {
            Point p = new Point();
            for (int i = 20; i <= 70; i++)
            {
                p.X = i;
                p.Y = (int)(40 - Math.Sqrt(-Math.Pow((double)i, 2.0) + 80 * i - 700));
                _points.Add(p);
                p.X = i;
                p.Y = (int)(Math.Sqrt(-Math.Pow((double)i, 2.0) + 80 * i - 700) + 40);
                _points.Add(p);
            }
            for (int i = 20; i <= 70; i++)
            {
                p.Y = i;
                p.X = (int)(40 - Math.Sqrt(-Math.Pow((double)i, 2.0) + 80 * i - 700));
                _points.Add(p);
                p.Y = i;
                p.X = (int)(Math.Sqrt(-Math.Pow((double)i, 2.0) + 80 * i - 700) + 40);
                _points.Add(p);
            }
            return _points;
        }
    }
}
