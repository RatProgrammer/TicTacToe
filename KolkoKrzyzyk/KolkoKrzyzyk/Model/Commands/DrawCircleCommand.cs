using System.Drawing;

namespace TicTacToe.Model.Commands
{
    class DrawCircleCommand : IPainterCommand
    {
        private readonly Pen _pen;

        public DrawCircleCommand(Pen pen)
        {
            _pen = pen;
        }

        public void Execute(ref Bitmap bitmap)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawEllipse(_pen, 50 - 45, 50 - 45, 45 + 45, 45 + 45);
            }
        }
    }
}
