using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AForge.Neuro;
using AForge.Neuro.Learning;
using TicTacToe.Model;
using TicTacToe.Model.DrawModel;
using TicTacToe.Model.NeuralModel;
using TicTacToe.View;

namespace TicTacToe.Presenter
{
    class TicTacToePresenter
    {
        private readonly View.TicTacToe _ticTacToe;
        private readonly MyPen _myPen;
        private  readonly LearningCanvas _learningCanvas;
        private readonly ChromaticImage _chromaticImage;
        private LearningContainer _learningContainer;
        private ActivationNetwork _network;
        private BackPropagationLearning _teacher;
        private readonly DesignateCircle _designateCircle;
        private readonly DesignateCross _designateCross;

        public TicTacToePresenter(View.TicTacToe ticTacToe)
        {
            _ticTacToe = ticTacToe;
            _myPen = new MyPen();
            _designateCircle = new DesignateCircle();
            _designateCross = new DesignateCross();
            _learningCanvas = new LearningCanvas();
            _learningContainer = new LearningContainer();
            _network = new ActivationNetwork(new SigmoidFunction(2), 100, 14, 2);
            _teacher = new BackPropagationLearning((ActivationNetwork)_network);
            _ticTacToe.StartPaintAction += ExecuteStartPaintAction;
            _ticTacToe.MovePaintAction += ExecuteMovePaintAction;
            _ticTacToe.StopPaintAction += ExecuteStopPaintAction;
            _ticTacToe.LearnAction += ExecuteLearnAction;
            _ticTacToe.CrossAction += ExecuteCrossAction;
            _ticTacToe.CircleAction += ExecuteCircleAction;
            _ticTacToe.ClearAction += ExecuteClearAction;
            _ticTacToe.CopyAction += ExecuteCopyAction;
            _ticTacToe.TestAction += ExecuteTestAction;
        }


        private void ExecuteTestAction()
        {
            NetworkTesting networkTesting = new NetworkTesting();
            var currentBitmap = networkTesting.Test(ref _learningContainer,  _learningCanvas, ref _network, _myPen, ref _teacher);
            UpdateCanvas(currentBitmap, CanvasType.pcResult);
        }

        private void ExecuteLearnAction()
        { 
            _ticTacToe.ShowMessage("Trwa nauka. \n Proszę czekać.");
            NetworkLearning networkLearning = new NetworkLearning();
            networkLearning.Learn(_learningContainer, _learningCanvas, ref _network, ref _teacher);
            _ticTacToe.ShowMessage("");
        }
        private void ExecuteStartPaintAction(Point point, CanvasType canvasType)
        {
            var currentBitmap = _learningCanvas.GetCanvas(canvasType);
            _myPen.ExecuteStart(ref currentBitmap, point);
            _learningCanvas.UpdateCanvas(currentBitmap, canvasType);
            UpdateCanvas(currentBitmap, canvasType);
        }

        private void ExecuteMovePaintAction(Point point, CanvasType canvasType)
        {
            var currentBitmap = _learningCanvas.GetCanvas(canvasType);
                _myPen.ExecuteMove(ref currentBitmap, point);
                _learningCanvas.UpdateCanvas(currentBitmap, canvasType);
                UpdateCanvas(currentBitmap, canvasType);
        }
        private void ExecuteStopPaintAction(Point point, CanvasType canvasType)
        {
            var currentBitmap = _learningCanvas.GetCanvas(canvasType);
            _myPen.ExecuteStop(ref currentBitmap, point);
            _learningCanvas.UpdateCanvas(currentBitmap, canvasType);
            UpdateCanvas(currentBitmap, canvasType);
        }

        private void ExecuteClearAction(CanvasType canvasType)
        {
            var currentBitmap = new Bitmap(100, 100);
            _learningCanvas.UpdateCanvas(currentBitmap, canvasType);
            UpdateCanvas(currentBitmap, canvasType);
        }
        private void ExecuteCopyAction(CanvasType canvasType)
        {
            var currentBitmap = _learningCanvas.GetCanvas(canvasType);
            _learningCanvas.UpdateCanvas(currentBitmap, CanvasType.pcTest);
            UpdateCanvas(currentBitmap, CanvasType.pcTest);
        }

        private void ExecuteCrossAction()
        {
            var currentBitmap = _designateCross.DrawCross(_learningCanvas, _myPen);
            UpdateCanvas(currentBitmap, CanvasType.pcCross);
        }
        private void ExecuteCircleAction()
        {
            var currentBitmap = _designateCircle.DrawCircle(_learningCanvas, _myPen);
            UpdateCanvas(currentBitmap, CanvasType.pcCircle);
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
                case CanvasType.pcCross:
                    _ticTacToe.UpdateCanvasCross(bitmap);
                    break;
                case CanvasType.pcCircle:
                    _ticTacToe.UpdateCanvasCircle(bitmap);
                    break;
                case CanvasType.pcBlank:
                    _ticTacToe.UpdateCanvasBlank(bitmap);
                    break;
                case CanvasType.pcTest:
                    _ticTacToe.UpdateCanvasTest(bitmap);
                    break;
                case CanvasType.pcResult:
                    _ticTacToe.UpdateCanvasResult(bitmap);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }


    }
}
