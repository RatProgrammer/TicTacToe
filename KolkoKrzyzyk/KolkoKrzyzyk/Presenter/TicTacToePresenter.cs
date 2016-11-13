using System;
using System.Drawing;
using System.Windows.Forms;
using KolkoKrzyzyk.Model.DrawModel;

namespace KolkoKrzyzyk.Presenter
{
    class TicTacToePresenter
    {
        private readonly TicTacToe _ticTacToe;
        private readonly MyPen _myPen;
        private Bitmap _currentBitmap;
        private readonly LearningCanvas _learningCanvas;

        public TicTacToePresenter(TicTacToe ticTacToe)
        {
            _ticTacToe = ticTacToe;
            _currentBitmap = new Bitmap(100,100);
            _myPen = new MyPen();
            _learningCanvas = new LearningCanvas();
            _ticTacToe.StartPaintAction += ExecuteStartPaintAction;
            _ticTacToe.MovePaintAction += ExecuteMovePaintAction;
            _ticTacToe.StopPaintAction += ExecuteStopPaintAction;
        }

        private void ExecuteStopPaintAction(Point point, CanvasType canvasType)
        { 
            _currentBitmap = _learningCanvas.GetCanvas(canvasType);
            _myPen.ExecuteStop(ref _currentBitmap, point);
            _learningCanvas.UpdateCanvas(_currentBitmap, canvasType);
            UpdateCanvas(_currentBitmap, canvasType);
        }

        private void ExecuteMovePaintAction(Point point, CanvasType canvasType)
        {
            _currentBitmap = _learningCanvas.GetCanvas(canvasType);
            _myPen.ExecuteMove(ref _currentBitmap, point);
            _learningCanvas.UpdateCanvas(_currentBitmap, canvasType);
            UpdateCanvas(_currentBitmap, canvasType);
        }

        private void ExecuteStartPaintAction(Point point, CanvasType canvasType)
        {
            _currentBitmap = _learningCanvas.GetCanvas(canvasType);
            _myPen.ExecuteStart(ref _currentBitmap, point);
            _learningCanvas.UpdateCanvas(_currentBitmap, canvasType);
            UpdateCanvas(_currentBitmap, canvasType);
        }
           public void RunApp()
        {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(_ticTacToe);
        }
        public void UpdateCanvas(Bitmap bitmap, CanvasType canvasType)
        {
            switch (canvasType)
            {
                case CanvasType.CCX:
                    _ticTacToe.UpdateCanvasX(bitmap);
                    break;
                case CanvasType.CCO:
                    _ticTacToe.UpdateCanvasO(bitmap);
                    break;
                case CanvasType.CCBlank:
                    _ticTacToe.UpdateCanvasBlank(bitmap);
                    break;
                case CanvasType.CCTest:
                    _ticTacToe.UpdateCanvasTest(bitmap);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }
    }
}
