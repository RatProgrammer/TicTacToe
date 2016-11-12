using System.Drawing;

namespace KolkoKrzyzyk.Model.DrawModel
{
    class MyPen
    {
        private readonly Pen _pen;

        public MyPen()
        {
            _pen = new Pen(Color.Black, 3);
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
                _graphics.DrawLine(_pen, startPoint, _previousPoint);
            }
            _previousPoint = point;
        }
    }
}
