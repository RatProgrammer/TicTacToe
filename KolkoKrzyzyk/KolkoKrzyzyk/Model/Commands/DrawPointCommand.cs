using System.Drawing;

namespace TicTacToe.Model.Commands
{
    class DrawPointCommand : IPainterCommand
    {
        private Point _point;
        private readonly Pen _pen;
        public DrawPointCommand(Point point, Pen pen)
        {
            _point = point;
            _pen = pen;
        }
        public void Execute(ref Bitmap bitmap)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawEllipse(_pen, _point.X, _point.Y, 5, 5);
            }
        }
    }
}
