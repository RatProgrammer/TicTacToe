using System.Drawing;
using System.Linq;
using TicTacToe.Model.DrawModel;

namespace TicTacToe.Model.Commands
{
    class DrawCrossCommand : IPainterCommand
    {
        private readonly Pen _pen;
        private readonly CrossDesignator _crossDesignator;


        public DrawCrossCommand(Pen pen, CrossDesignator crossDesignator)
        {
            _pen = pen;
            _crossDesignator = crossDesignator;
        }

        public void Execute(ref Bitmap bitmap)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                var points = _crossDesignator.Designate();
                for (int i = 0; i < points.Count; i++)
                {
                    graphics.DrawEllipse(_pen, points.ElementAt(i).X, points.ElementAt(i).Y, 5, 5);
                }
            }
        }
    }
}
