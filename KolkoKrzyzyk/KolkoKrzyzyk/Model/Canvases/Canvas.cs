using System.Drawing;
using TicTacToe.Model.Commands;

namespace TicTacToe.Model.Canvases
{
    class Canvas
    {
        private const int Width = 100;
        private const int Hight = 100;

        private Bitmap _bitmap;

        public Canvas()
        {
            _bitmap = new Bitmap(Width, Hight);
        }

        public Bitmap GetBitmap()
        {
            return new Bitmap(_bitmap);
        }

        public void CopyCanvas(Canvas canvas)
        {
            _bitmap = canvas.GetBitmap();
        }

        public void ClearCanvas()
        {
            _bitmap = new Bitmap(Width, Hight);
        }

        public void DrawOnCanvas(IPainterCommand command)
        {
            command.Execute(ref _bitmap);
        }
    }
}
