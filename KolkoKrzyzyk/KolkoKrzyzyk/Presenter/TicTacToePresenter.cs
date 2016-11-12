using System;
using System.Drawing;
using System.Windows.Forms;
using KolkoKrzyzyk.Model.DrawModel;
using KolkoKrzyzyk.View;

namespace KolkoKrzyzyk.Presenter
{
    class TicTacToePresenter
    {
        private TicTacToe _ticTacToe;
        private MyPen _myPen;
        private Bitmap _currentBitmap;

        public TicTacToePresenter(TicTacToe ticTacToe)
        {
            _ticTacToe = ticTacToe;
            _currentBitmap = new Bitmap(100,100);
            _myPen = new MyPen();
            _ticTacToe.StartPaintAction += ExecuteStartPaintAction;
            _ticTacToe.MovePaintAction += ExecuteMovePaintAction;
            _ticTacToe.StopPaintAction += ExecuteStopPaintAction;
        }

        private void ExecuteStopPaintAction(Point point, CanvasType canvasType)
        { 
           _myPen.ExecuteStop(ref _currentBitmap, point);
            _ticTacToe.UpdateCanvas(canvasType,_currentBitmap);
        }

        private void ExecuteMovePaintAction(Point point, CanvasType canvasType)
        {
            _myPen.ExecuteMove(ref _currentBitmap, point);
            _ticTacToe.UpdateCanvas(canvasType, _currentBitmap);
        }

        private void ExecuteStartPaintAction(Point point, CanvasType canvasType)
        {
            _myPen.ExecuteStart(ref _currentBitmap, point);
            _ticTacToe.UpdateCanvas(canvasType, _currentBitmap);
        }
           public void RunApp()
        {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(_ticTacToe);
        }
    }
}
