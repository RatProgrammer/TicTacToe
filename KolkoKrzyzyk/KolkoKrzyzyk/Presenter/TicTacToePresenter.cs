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

        public TicTacToePresenter(View.TicTacToe ticTacToe)
        {
            _ticTacToe = ticTacToe;
            _myPen = new MyPen();
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
            NetworkTesting networkTesting = new NetworkTesting();
            var currentBitmap = networkTesting.Test(ref _learningContainer,  _learningCanvas, ref _network, _myPen, ref _teacher);
            UpdateCanvas(currentBitmap, CanvasType.pcResult);

            // _learningContainer.SetChromaticImage(_learningCanvas);
            // var bitmapToTest = _learningContainer.GetChromaticImage(CanvasType.pcTest);
            // double[] testInput = bitmapToTest.GetChromaticImage();
            //// Test();
            // double[] netout = _network.Compute(testInput);
            // if (netout[0] > 0.6 && netout[1] < 0.1)
            // {
            //     DesignateCross designateCross = new DesignateCross();
            //     List<Point> points = designateCross.Designate();
            //     var currentBitmap = _myPen.DrawShape(points, _learningCanvas.GetCanvas(CanvasType.pcCross));
            //     _learningCanvas.UpdateCanvas(currentBitmap, CanvasType.pcTest);
            //     UpdateCanvas(currentBitmap, CanvasType.pcResult);
            //     MessageBox.Show("Krzyżyk");

            // }
            // else if (netout[0] < 0.1 && netout[1] > 0.6)
            // {
            //     DesignateCircle designateCircle = new DesignateCircle();
            //     List<Point> points = designateCircle.Designate();
            //     var currentBitmap = _myPen.DrawShape(points, _learningCanvas.GetCanvas(CanvasType.pcCircle));
            //     _learningCanvas.UpdateCanvas(currentBitmap, CanvasType.pcTest);
            //     UpdateCanvas(currentBitmap, CanvasType.pcResult);
            //     MessageBox.Show("Kółko");
            // }
            // else
            // {
            //     MessageBox.Show("Pusty");
            // }
            // MessageBox.Show("Skończony test");

        }

        private void ExecuteCopyAction(CanvasType canvasType)
        {
            var currentBitmap = _learningCanvas.GetCanvas(canvasType);
            _learningCanvas.UpdateCanvas(currentBitmap,CanvasType.pcTest);
            UpdateCanvas(currentBitmap,CanvasType.pcTest);
        }

        private void Test()
        {
            _network = (ActivationNetwork)ActivationNetwork.Load("Net.bin");
            _teacher = new BackPropagationLearning((ActivationNetwork)_network);
        }


        private void ExecuteLearnAction()
        {
            NetworkLearning networkLearning = new NetworkLearning();
            networkLearning.Learn(_learningContainer, _learningCanvas, ref _network, ref _teacher);

            //_learningContainer.SetChromaticImage(_learningCanvas);
            //var crossImage = _learningContainer.GetChromaticImage(CanvasType.pcCross);
            //double[] crossInput = crossImage.GetChromaticImage();

            //var circleImage = _learningContainer.GetChromaticImage(CanvasType.pcCircle);
            //double[] circleInput = circleImage.GetChromaticImage();


            //var blankImage = _learningContainer.GetChromaticImage(CanvasType.pcBlank);
            //double[] blankInput = blankImage.GetChromaticImage();
            //double[][] input = new double[22][]
            //{
            //    crossInput,
            //    circleInput,
            //    blankInput,
            //    new double[]
            //    {
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //blank
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            //    },
            //    new double[]
            //    {
            //        0, 0, 0, 1, 1, 1, 0, 0, 0, 0, //circle
            //        0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
            //        0, 0, 1, 0, 0, 0, 0, 1, 0, 0,
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
            //        0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
            //        0, 0, 0, 1, 1, 1, 0, 0, 0, 0
            //    },

            //    new double[]
            //    {
            //        0, 0, 0, 0, 1, 1, 0, 0, 0, 0, //circle
            //        0, 0, 0, 1, 0, 0, 1, 0, 0, 0,
            //        0, 0, 1, 0, 0, 0, 0, 1, 0, 0,
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
            //        0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
            //        0, 0, 0, 1, 1, 1, 0, 0, 0, 0
            //    },
            //    new double[]
            //    {
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //cross
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 0, 1, 0, 0, 0, 0, 1, 0, 0,
            //        0, 0, 0, 1, 0, 0, 1, 0, 0, 0,
            //        0, 0, 0, 0, 1, 1, 0, 0, 0, 0,
            //        0, 0, 0, 0, 1, 1, 0, 0, 0, 0,
            //        0, 0, 0, 1, 0, 0, 1, 0, 0, 0,
            //        0, 0, 1, 0, 0, 0, 0, 1, 0, 0,
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            //    },
            //    new double[]
            //    {
            //        1, 1, 0, 0, 0, 0, 0, 0, 0, 0, //cross
            //        0, 1, 1, 0, 0, 0, 0, 0, 1, 0,
            //        0, 0, 1, 1, 0, 0, 0, 1, 0, 0,
            //        0, 0, 0, 1, 1, 0, 1, 0, 0, 0,
            //        0, 0, 0, 0, 1, 1, 0, 0, 0, 0,
            //        0, 0, 0, 0, 1, 1, 1, 0, 0, 0,
            //        0, 0, 0, 1, 0, 0, 1, 1, 0, 0,
            //        0, 0, 1, 0, 0, 0, 0, 1, 1, 0,
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 1,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 1
            //    },
            //    new double[]
            //    {
            //        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, //cross
            //        1, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 1, 1, 0, 0, 0, 0, 1, 0, 0,
            //        0, 0, 1, 1, 0, 0, 1, 0, 0, 0,
            //        0, 0, 0, 1, 1, 1, 0, 0, 0, 0,
            //        0, 0, 0, 0, 1, 1, 0, 0, 0, 0,
            //        0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
            //        0, 0, 1, 0, 0, 0, 1, 1, 0, 0,
            //        0, 1, 0, 0, 0, 0, 0, 1, 1, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 1, 1
            //    },
            //    new double[]
            //    {
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, //blank
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1
            //    },
            //    new double[]
            //    {
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, //blank
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            //    },
            //    new double[]
            //    {
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //blank
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            //    },
            //    new double[]
            //    {
            //        0, 0, 0, 1, 1, 1, 0, 0, 0, 0, //circle
            //        0, 0, 1, 1, 1, 1, 1, 0, 0, 0,
            //        0, 1, 1, 0, 0, 0, 0, 1, 0, 0,
            //        1, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        1, 1, 0, 0, 0, 0, 0, 0, 1, 1,
            //        1, 1, 0, 0, 0, 0, 0, 0, 1, 1,
            //        1, 1, 0, 0, 0, 0, 0, 0, 1, 1,
            //        0, 1, 0, 0, 0, 0, 0, 1, 1, 0,
            //        0, 1, 1, 0, 0, 1, 1, 1, 0, 0,
            //        0, 0, 1, 1, 1, 1, 1, 0, 0, 0
            //    },
            //    new double[]
            //    {
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //circle
            //        0, 0, 1, 1, 1, 1, 1, 0, 0, 0,
            //        0, 1, 1, 0, 0, 0, 0, 1, 0, 0,
            //        1, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            //        1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            //        1, 1, 0, 0, 0, 0, 0, 0, 0, 1,
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 1, 0, 0, 0, 0, 1, 1, 0, 0,
            //        0, 0, 1, 1, 1, 1, 1, 0, 0, 0
            //    },
            //    new double[]
            //    {
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //circle
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
            //        0, 0, 1, 0, 0, 0, 0, 1, 0, 0,
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
            //        0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
            //        0, 0, 0, 1, 1, 1, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            //    },
            //    new double[]
            //    {
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //cross
            //        0, 0, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 0, 1, 0, 0, 0, 0, 1, 1, 0,
            //        0, 0, 0, 1, 0, 0, 1, 1, 0, 0,
            //        0, 0, 0, 1, 1, 1, 0, 0, 0, 0,
            //        0, 0, 0, 0, 1, 1, 1, 0, 0, 0,
            //        0, 0, 0, 1, 0, 0, 1, 0, 0, 0,
            //        0, 0, 1, 0, 0, 0, 1, 1, 0, 0,
            //        0, 1, 1, 0, 0, 0, 0, 1, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            //    },
            //    new double[]
            //    {
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //circle
            //        0, 0, 0, 1, 1, 1, 0, 0, 0, 0,
            //        0, 0, 1, 0, 1, 1, 1, 0, 0, 0,
            //        0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
            //        0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
            //        0, 0, 0, 1, 1, 1, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            //    },
            //    new double[]
            //    {
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //circle
            //        0, 0, 1, 1, 1, 1, 0, 0, 0, 0,
            //        0, 1, 1, 1, 1, 1, 1, 0, 0, 0,
            //        0, 1, 1, 1, 0, 1, 0, 0, 0, 0,
            //        0, 1, 1, 0, 0, 1, 1, 0, 0, 0,
            //        1, 1, 1, 0, 0, 1, 1, 0, 0, 0,
            //        1, 1, 1, 0, 1, 1, 0, 0, 0, 0,
            //        1, 1, 1, 1, 1, 1, 0, 0, 0, 0,
            //        0, 1, 1, 1, 1, 0, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            //    },
            //    new double[]
            //    {
            //        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, //cross
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 0, 1, 0, 0, 0, 0, 1, 1, 0,
            //        0, 0, 0, 1, 0, 0, 1, 1, 0, 0,
            //        0, 0, 0, 1, 1, 1, 0, 0, 0, 0,
            //        0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
            //        0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
            //        0, 1, 0, 0, 0, 0, 1, 1, 0, 0,
            //        1, 0, 0, 0, 0, 0, 0, 1, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 1, 0
            //    },
            //    new double[]
            //    {
            //        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, //cross
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        0, 0, 1, 0, 0, 0, 0, 1, 1, 0,
            //        0, 0, 0, 1, 0, 0, 1, 1, 0, 0,
            //        0, 0, 0, 1, 1, 1, 0, 0, 0, 0,
            //        0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
            //        0, 0, 1, 0, 0, 0, 0, 1, 0, 0,
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            //    },
            //    new double[]
            //    {
            //        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, //cross
            //        0, 1, 0, 0, 0, 0, 0, 0, 0, 1,
            //        0, 0, 1, 0, 0, 0, 0, 0, 1, 0,
            //        0, 0, 0, 1, 0, 0, 0, 1, 0, 0,
            //        0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
            //        0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
            //        0, 0, 1, 0, 0, 0, 0, 1, 0, 0,
            //        0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
            //        1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            //    },
            //    new double[]
            //    {
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //circle
            //        0, 0, 1, 1, 1, 1, 0, 0, 0, 0,
            //        0, 1, 1, 0, 1, 1, 1, 0, 0, 0,
            //        0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
            //        0, 1, 0, 0, 0, 0, 1, 1, 1, 0,
            //        0, 1, 0, 0, 0, 0, 1, 1, 0, 0,
            //        0, 0, 1, 0, 0, 0, 1, 1, 0, 0,
            //        0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
            //        0, 0, 0, 0, 1, 1, 0, 0, 0, 0,
            //        0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            //    },

            //};
            //double[][] output = new double[22][]
            //{
            //    //8 cross 8 circle 6 blank
            //    new double[] {1, 0}, //cross
            //    new double[] {0, 1}, //circle
            //    new double[] {0, 0}, //blank
            //    new double[] {0, 0}, //blank
            //    new double[] {0, 1}, //circle
            //    new double[] {0, 1}, //circle
            //    new double[] {1, 0}, //cross
            //    new double[] {1, 0}, //cross
            //    new double[] {1, 0}, //cross
            //    new double[] {0, 0}, //blank
            //    new double[] {0, 0}, //blank
            //    new double[] {0, 0}, //blank
            //    new double[] {0, 1}, //circle
            //    new double[] {0, 1}, //circle
            //    new double[] {0, 1}, //circle
            //    new double[] {1, 0}, //cross
            //    new double[] {0, 1}, //circle
            //    new double[] {0, 1}, //circle
            //    new double[] {1, 0}, //cross
            //    new double[] {1, 0}, //cross
            //    new double[] {1, 0}, //cross
            //    new double[] {0, 1}, //circle
            //};

            //for (int i = 0; i < 100000; i++)
            //{
            //    double error = _teacher.RunEpoch(input, output);
            //}

            //_network.Save("Net.bin");
            //_network = (ActivationNetwork)ActivationNetwork.Load("Net.bin");

            //MessageBox.Show("Skończona nauka");

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
                case CanvasType.pcResult:
                    _ticTacToe.UpdateCanvasResult(bitmap);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }
    }
}
