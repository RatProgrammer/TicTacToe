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
                case CanvasType.pcCross:
                    _crossBitmap = new Bitmap(bitmap);
                    break;
                case CanvasType.pcCircle:
                    _circleBitmap = new Bitmap(bitmap); ;
                    break;
                case CanvasType.pcBlank:
                    _blankBitmap = new Bitmap(bitmap); ;
                    break;
                case CanvasType.pcTest:
                    _testBitmap = new Bitmap(bitmap); ;
                    break;
                case CanvasType.pcResult:
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
                case CanvasType.pcCross:
                    return new Bitmap(_crossBitmap);
                case CanvasType.pcCircle:
                    return new Bitmap(_circleBitmap);
                case CanvasType.pcBlank:
                    return new Bitmap(_blankBitmap);
                case CanvasType.pcTest:
                    return new Bitmap(_testBitmap);
                case CanvasType.pcResult:
                    return new Bitmap(_result);
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }
    }
}
