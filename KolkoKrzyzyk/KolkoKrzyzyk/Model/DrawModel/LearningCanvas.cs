using System;
using System.Drawing;

namespace KolkoKrzyzyk.Model.DrawModel
{
    class LearningCanvas
    {
        private Bitmap _testBitmap;
        private Bitmap _crossBitmap;
        private Bitmap _circleBitmap;
        private Bitmap _blankBitmap;

        public LearningCanvas()
        {
            _testBitmap = new Bitmap(100,100);
            _crossBitmap = new Bitmap(100, 100);
            _circleBitmap = new Bitmap(100, 100);
            _blankBitmap = new Bitmap(100, 100);
        }

        public void UpdateCanvas(Bitmap bitmap, CanvasType canvasType)
        {
            switch (canvasType)
            {
                case CanvasType.CCX:
                    _crossBitmap = bitmap;
                    break;
                case CanvasType.CCO:
                    _circleBitmap = bitmap;
                    break;
                case CanvasType.CCBlank:
                    _blankBitmap = bitmap;
                    break;
                case CanvasType.CCTest:
                    _testBitmap = bitmap;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }



        public Bitmap GetCanvas (CanvasType canvasType)
        {
            switch (canvasType)
            {
                case CanvasType.CCX:
                    return new Bitmap(_crossBitmap);
                case CanvasType.CCO:
                    return new Bitmap(_circleBitmap);
                case CanvasType.CCBlank:
                    return new Bitmap(_blankBitmap);
                case CanvasType.CCTest:
                    return new Bitmap(_testBitmap);
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }
    }
}
