using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AForge.Neuro;
using AForge.Neuro.Learning;
using TicTacToe.Model;
using TicTacToe.Model.DrawModel;
using TicTacToe.View;

namespace TicTacToe.Presenter
{
    class TicTacToePresenter
    {
        private readonly View.TicTacToe _ticTacToe;
        private readonly MyPen _myPen;
        private Bitmap _currentBitmap;
        private readonly LearningCanvas _learningCanvas;
        private ChromaticImage _chromaticImage;
        private LearningContainer _learningContainer;
        private ActivationNetwork _network;
        private BackPropagationLearning _teacher;
        private Network net;

        public TicTacToePresenter(View.TicTacToe ticTacToe)
        {
            _ticTacToe = ticTacToe;
            _currentBitmap = new Bitmap(100,100);
            _myPen = new MyPen();
            _learningCanvas = new LearningCanvas();
            _learningContainer = new LearningContainer();
            _network = new ActivationNetwork(new SigmoidFunction(2), 10000, 10, 2);
            //
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

        private void ExecuteCircleAction()
        {
            DesignateCircle designateCircle = new DesignateCircle();
            List<Point> points = designateCircle.Designate();
            var currentBitmap = _myPen.DrawShape(points, _learningCanvas.GetCanvas(CanvasType.pcCircle));
            _learningCanvas.UpdateCanvas(currentBitmap, CanvasType.pcCircle);
            UpdateCanvas(currentBitmap, CanvasType.pcCircle);
        }

        private void ExecuteTestAction()
        {
            var bitmapToTest = _learningContainer.GetChromaticImage(CanvasType.pcTest);
            double[] testInput = bitmapToTest.GetChromaticImage();
            ExecuteLearnAction();
            double[] netout = _network.Compute(testInput);
            if (netout[0] > 0.7 && netout[1] < 0.1)
            {
               ExecuteCrossAction();

            }
            else if (netout[0] < 0.1 && netout[1] > 0.7)
            {
                ExecuteCircleAction();
            }
            else
            {
                
            }
            //gppForResult.DrawShaps();
        }

        private void ExecuteCopyAction(CanvasType canvasType)
        {
            var currentBitmap = _learningCanvas.GetCanvas(canvasType);
            _learningCanvas.UpdateCanvas(currentBitmap,CanvasType.pcTest);
            UpdateCanvas(currentBitmap,CanvasType.pcTest);
        }


        private void ExecuteLearnAction()
        {

            try
            {
                _network = (ActivationNetwork)ActivationNetwork.Load("Net.bin");
            }
            catch (Exception e)
            {
                net = new ActivationNetwork(new SigmoidFunction(2), 10000, 20, 2);
                _teacher = new BackPropagationLearning((ActivationNetwork)_network);
                _learningContainer.SetChromaticImage(_learningCanvas);
                var crossImage = _learningContainer.GetChromaticImage(CanvasType.pcCross);
                double[] crossInput = crossImage.GetChromaticImage();

                var circleImage = _learningContainer.GetChromaticImage(CanvasType.pcCircle);
                double[] circleInput = circleImage.GetChromaticImage();


                var blankImage = _learningContainer.GetChromaticImage(CanvasType.pcBlank);
                double[] blankInput = blankImage.GetChromaticImage();
                double[][] input = new double[3][]
                {
                crossInput,
                circleInput,
                blankInput
                };
                double[][] output = new double[3][]
                {
                new double[] {1,0},
                new double[] {0,1},
                new double[] {0,0}
                };

                for (int i = 0; i < 10000; i++)
                {
                    // run epoch of learning procedure
                    double error = _teacher.RunEpoch(input, output);
                    // check error value to see if we need to stop
                    // ...
                }
                _network.Save("Net.bin");
                _network = (ActivationNetwork)ActivationNetwork.Load("Net.bin");

                var result1 = _network.Compute(input[0]);
                var result2 = _network.Compute(input[1]);
                var result3 = _network.Compute(input[2]);

            }
           _teacher = new BackPropagationLearning((ActivationNetwork)_network);
        }

        private void ExecuteStopPaintAction(Point point, CanvasType canvasType)
        {
            var currentBitmap = _learningCanvas.GetCanvas(canvasType);
            _myPen.ExecuteStop(ref currentBitmap, point);
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

        private void ExecuteStartPaintAction(Point point, CanvasType canvasType)
        {
            var currentBitmap = _learningCanvas.GetCanvas(canvasType);
            _myPen.ExecuteStart(ref currentBitmap, point);
            _learningCanvas.UpdateCanvas(currentBitmap, canvasType);
            UpdateCanvas(currentBitmap, canvasType);
        }
        private void ExecuteClearAction(CanvasType canvasType)
        {
            var currentBitmap = new Bitmap(100, 100);
            _learningCanvas.UpdateCanvas(currentBitmap, canvasType);
            UpdateCanvas(currentBitmap, canvasType);
        }

        private void ExecuteCrossAction()
        {
            DesignateCross designateCross = new DesignateCross();
            List<Point> points = designateCross.Designate();
            var currentBitmap = _myPen.DrawShape(points, _learningCanvas.GetCanvas(CanvasType.pcCross));
            _learningCanvas.UpdateCanvas(currentBitmap, CanvasType.pcCross);
            UpdateCanvas(currentBitmap, CanvasType.pcCross);
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }
    }
}
