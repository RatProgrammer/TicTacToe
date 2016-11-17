using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TicTacToe.Model.DrawModel
{
    class MyPen
    {
        private readonly Pen _pen;

        public MyPen()
        {
            _pen = new Pen(Color.Black, 10);
        }
        private Point _previousPoint;
        private Graphics _graphics;

        public void ExecuteStart(ref Bitmap current, Point point)
        {

            Point startPoint = point;
            current.SetPixel(startPoint.X, startPoint.Y, _pen.Color);
            _previousPoint = point;
        }

        public void ExecuteStop(ref Bitmap current, Point point)
        {
            _previousPoint = new Point(0, 0);
        }

        public void ExecuteMove(ref Bitmap current, Point point)
        {
            Point startPoint = point;
            using (_graphics = Graphics.FromImage(current))
            {
                _graphics.DrawEllipse(_pen, point.X, point.Y, 5, 5);
            }
            _previousPoint = point;
        }

        public Bitmap DrawShape(List<Point> points, Bitmap bitmap)
        {
            using (_graphics = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < points.Count; i++)
                {
                    _graphics.DrawEllipse(_pen, points.ElementAt(i).X, points.ElementAt(i).Y,5,5);
                }
                return bitmap;
            }
        }
    }
}
