using System.Collections.Generic;
using System.Drawing;

namespace TicTacToe.Model.DrawModel
{
    class DesignateCross
    {
        private readonly List<Point> _points;

        public DesignateCross()
        {
            _points = new List<Point>();
        }

        public List<Point> Designate()
        {
            Point p = new Point();
            for (int i = 10; i < 90; i++)
            {
                for (int j = 10; j < 90; j++)
                {
                    if (i == j)
                    {
                        p.X = i;
                        p.Y = j;
                        _points.Add(p);
                        p.X = i;
                        p.Y = 100 - i;
                        _points.Add(p);
                    }
                }
            }
            return _points;
        }
    }
}
