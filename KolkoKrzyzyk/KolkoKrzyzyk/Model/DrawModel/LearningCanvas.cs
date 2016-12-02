using System;
using System.Drawing;
using TicTacToe.View;

namespace TicTacToe.Model.DrawModel
{
    class LearningCanvas
    {
        private Bitmap _testBitmap;
        private Bitmap _crossBitmap;
        private Bitmap _circleBitmap;
        private Bitmap _blankBitmap;
        private Bitmap _result;

        public LearningCanvas()
        {
            _testBitmap = new Bitmap(100,100);
            _crossBitmap = new Bitmap(100, 100);
            _circleBitmap = new Bitmap(100, 100);
            _blankBitmap = new Bitmap(100, 100);
            _result = new Bitmap(100, 100);
        }

        public void UpdateCanvas(Bitmap bitmap, CanvasType canvasType)
        {
            switch (canvasType)
            {
                case CanvasType.Cross:
                    _crossBitmap = new Bitmap(bitmap);
                    break;
                case CanvasType.Circle:
                    _circleBitmap = new Bitmap(bitmap); ;
                    break;
                case CanvasType.Blank:
                    _blankBitmap = new Bitmap(bitmap); ;
                    break;
                case CanvasType.Test:
                    _testBitmap = new Bitmap(bitmap); ;
                    break;
                case CanvasType.Result:
                    _result = new Bitmap(bitmap);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }

        public Bitmap GetCanvas (CanvasType canvasType)
        {
            switch (canvasType)
            {
                case CanvasType.Cross:
                    return new Bitmap(_crossBitmap);
                case CanvasType.Circle:
                    return new Bitmap(_circleBitmap);
                case CanvasType.Blank:
                    return new Bitmap(_blankBitmap);
                case CanvasType.Test:
                    return new Bitmap(_testBitmap);
                case CanvasType.Result:
                    return new Bitmap(_result);
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }
    }
}
