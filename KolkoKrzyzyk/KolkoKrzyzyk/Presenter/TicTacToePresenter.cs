using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AForge.Neuro;
using AForge.Neuro.Learning;
using TicTacToe.Model;
using TicTacToe.Model.Canvases;
using TicTacToe.Model.Commands;
using TicTacToe.Model.DrawModel;
using TicTacToe.Model.GameModel;
using TicTacToe.Model.LearnModel;
using TicTacToe.Model.NeuralModel;
using TicTacToe.Model.Utility;
using TicTacToe.View;

namespace TicTacToe.Presenter
{
    class TicTacToePresenter
    {
        private readonly TicTacToeForm _ticTacToeForm;
        private  readonly CanvasContainer _canvasContainer;
        private ActivationNetwork _network;
        private BackPropagationLearning _teacher;               
        private IPainterCommand _painterCommand;
        private Pen _pen;
        private CrossDesignator _crossDesignator;
        private Game _game;
        public TicTacToePresenter(TicTacToeForm ticTacToeForm)
        {
            _ticTacToeForm = ticTacToeForm;
            _canvasContainer = new CanvasContainer();
            _network = new ActivationNetwork(new SigmoidFunction(2), 100, 14, 2);
            _teacher = new BackPropagationLearning(_network);
            _pen = new Pen(Color.Black, 5);
            _crossDesignator = new CrossDesignator();
            _network = (ActivationNetwork) Network.Load("Net.bin");
            _game = new Game();
            _ticTacToeForm.DrawAction += Draw;
            _ticTacToeForm.LearnAction += ExecuteLearnAction;
            _ticTacToeForm.CrossAction += ExecuteCrossAction;
            _ticTacToeForm.CircleAction += ExecuteCircleAction;
            _ticTacToeForm.ClearAction += ExecuteClearAction;
            _ticTacToeForm.CopyAction += ExecuteCopyAction;
            _ticTacToeForm.TestAction += ExecuteTestAction;
            _ticTacToeForm.NewGameAction += ExecuteNewGameAction;
            _ticTacToeForm.PlayAction += ExecutePlayAction;
            _game.ComputerPlayerWinAction += ExecuteComputerPlayerWinAction;
            _game.HumanPlayerWinAction += ExecuteHumanPlayerWinAction;
            _game.DrawAction += ExecuteGameDrawAction;


        }

        private void ExecuteGameDrawAction()
        {
            _ticTacToeForm.ShowMessage("Draw");
        }

        private void ExecuteHumanPlayerWinAction()
        {
            _ticTacToeForm.ShowMessage("You win");
        }

        private void ExecuteComputerPlayerWinAction()
        {
            _ticTacToeForm.ShowMessage("You lost");
        }

        private void ExecutePlayAction()
        {
            var list = EnumUtil.GetListOfEnumElement<CanvasType>("Matrix");
            NetworkTester networkTester = new NetworkTester(_network);
            List<GameMark> result = new List<GameMark>();
            foreach (var canvasType in list)
            {
                var canvas = _canvasContainer.GetCanvas(canvasType);;
                double[] chromaticCanvas = BitmapConverter.ImageToByte(canvas.GetBitmap());
                result.Add(networkTester.Test(chromaticCanvas));
            }
            
            var gameResult = _game.CheckGame(result.Select(x => (int)x).ToArray());
            if (gameResult > -1)
            {
                var matrix = list.ElementAt(gameResult);
                _painterCommand = new DrawCrossCommand(_pen, _crossDesignator);
                _canvasContainer.DrawOnCanvas(matrix, _painterCommand);
                UpdateCanvasView(matrix);
            }

        }

        private void ExecuteNewGameAction()
        {
            var list = EnumUtil.GetListOfEnumElement<CanvasType>("Matrix");
            foreach (var canvasType in list)
            {
                _canvasContainer.ClearCanvas(canvasType);
                UpdateCanvasView(canvasType);
            }
        }

        private void Draw(Point point, CanvasType canvasType)
        {
            _painterCommand = new DrawPointCommand(point, _pen);
            _canvasContainer.DrawOnCanvas(canvasType, _painterCommand);
            UpdateCanvasView(canvasType);
        }

        private void ExecuteTestAction()
        {
            _canvasContainer.ClearCanvas(CanvasType.Result);
            //LoadNetworkFromFile();
            NetworkTester networkTester = new NetworkTester(_network);
            var inputData = PreaperNetworkInput(CanvasType.Test);
            GameMark gameMark = networkTester.Test(inputData);
            if (gameMark == GameMark.Circle)
            {
                _painterCommand = new DrawCircleCommand(_pen);
                _canvasContainer.DrawOnCanvas(CanvasType.Result, _painterCommand);
                MessageBox.Show("Circle");
            }
            if (gameMark == GameMark.Cross)
            {
                _painterCommand = new DrawCrossCommand(_pen,_crossDesignator);
                _canvasContainer.DrawOnCanvas(CanvasType.Result, _painterCommand);
                MessageBox.Show("Cross");
            }
            else
            {
                MessageBox.Show("Blank");
            }
            UpdateCanvasView(CanvasType.Result);
        }

        private void ExecuteLearnAction()
        { 
            _ticTacToeForm.UpdateInformationLabel("Trwa nauka. \n Proszę czekać.");
            LoadNetworkFromFile();
            NetworkLearning networkLearning = new NetworkLearning();
            var crossInput = PreaperNetworkInput(CanvasType.Cross);
            var circleInput = PreaperNetworkInput(CanvasType.Circle);
            var blankInput = PreaperNetworkInput(CanvasType.Blank);
            networkLearning.Learn(ref _network, crossInput, circleInput, blankInput);
            _ticTacToeForm.UpdateInformationLabel("");
        }

        private double[] PreaperNetworkInput(CanvasType canvasType)
        {
            var canvas = _canvasContainer.GetCanvas(canvasType);
            var input = BitmapConverter.ImageToByte(canvas.GetBitmap());
            return input;
        }

        private void ExecuteClearAction(CanvasType canvasType)
        {
            _canvasContainer.ClearCanvas(canvasType);
            UpdateCanvasView(canvasType);
        }
        private void ExecuteCopyAction(CanvasType canvasType)
        {
            var currentBitmap = _canvasContainer.GetCanvas(CanvasType.Test);
            _canvasContainer.UpdateCanvas(currentBitmap, canvasType);
            UpdateCanvasView(canvasType);
        }

        private void ExecuteCrossAction()
        {
            _painterCommand = new DrawCrossCommand(_pen, _crossDesignator );
            _canvasContainer.DrawOnCanvas(CanvasType.Cross, _painterCommand);
            UpdateCanvasView(CanvasType.Cross);
        }
        private void ExecuteCircleAction()
        {
            _painterCommand = new DrawCircleCommand(_pen);
            _canvasContainer.DrawOnCanvas(CanvasType.Circle, _painterCommand);
            UpdateCanvasView(CanvasType.Circle);
        }
        public void RunApp()
        {
            Application.EnableVisualStyles();
            Application.Run(_ticTacToeForm);
        }

        public void UpdateCanvasView(CanvasType canvasType)
        {
            var canvas = _canvasContainer.GetCanvas(canvasType);
            var bitmap = canvas.GetBitmap();
          
            _ticTacToeForm.UpdatePictureBox($"pb{canvasType}", bitmap);
        }

        private void LoadNetworkFromFile()
        {
            _network = (ActivationNetwork)Network.Load("Net.bin");
        }


    }
}
