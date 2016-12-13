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
        private IPainterCommand _painterCommand;
        private readonly Pen _pen;
        private readonly CrossDesignator _crossDesignator;
        private Game _game;
        private ComputerPlayer _computerPlayer;
        private GameUtil _gameUtil;
        private readonly List<CanvasType> _gameCanvases;
        private CanvasType _lastModifiedCanvas;
        public TicTacToePresenter(TicTacToeForm ticTacToeForm)
        {
            _gameCanvases = EnumUtil.GetListOfEnumElement<CanvasType>("Matrix");
            _ticTacToeForm = ticTacToeForm;
            _canvasContainer = new CanvasContainer();
            _network = new ActivationNetwork(new SigmoidFunction(2), 100, 14, 2);
            new BackPropagationLearning(_network);
            _pen = new Pen(Color.Black, 5);
            _crossDesignator = new CrossDesignator();
            _network = (ActivationNetwork) Network.Load("Net.bin");
            _gameUtil = new GameUtil();
            _computerPlayer = new ComputerPlayer(_gameUtil ,_gameCanvases);
            _game = new Game(_gameUtil);
            _ticTacToeForm.DrawAction += Draw;
            _ticTacToeForm.LearnAction += ExecuteLearnAction;
            _ticTacToeForm.CrossAction += ExecuteCrossAction;
            _ticTacToeForm.CircleAction += ExecuteCircleAction;
            _ticTacToeForm.ClearAction += ExecuteClearAction;
            _ticTacToeForm.CopyAction += ExecuteCopyAction;
            _ticTacToeForm.TestAction += ExecuteTestAction;
            _ticTacToeForm.NewGameAction += ExecuteNewGameAction;
            _ticTacToeForm.PlayAction += ExecutePlayAction;
            _ticTacToeForm.BackAction += ExecuteBackAction;
            _lastModifiedCanvas = CanvasType.None;

        }

        private void ExecuteBackAction()
        {
            _canvasContainer.ClearCanvas(_lastModifiedCanvas);
            UpdateCanvasView(_lastModifiedCanvas);
        }

        private void GameInfo(string message)
        {
            _ticTacToeForm.ShowMessage(message);
            ExecuteNewGameAction();
        }

        private void ExecutePlayAction()
        {
            var borad = PrepareBoard();
            var move = _computerPlayer.PerformMove(borad);
            _canvasContainer.DrawOnCanvas(move, new DrawCrossCommand(_pen, _crossDesignator));
            UpdateCanvasView(move);
            PrepareBoard();
        }

        private int[] PrepareBoard()
        {
            NetworkTester networkTester = new NetworkTester(_network);
            List<GameMark> listOfGameMarks = new List<GameMark>();
            foreach (var canvasType in _gameCanvases)
            {
                var canvas = _canvasContainer.GetCanvas(canvasType);
                ;
                double[] chromaticCanvas = BitmapConverter.ImageToByte(canvas.GetBitmap());
                listOfGameMarks.Add(networkTester.Test(chromaticCanvas));
            }
            var borad = listOfGameMarks.Select(x => (int) x).ToArray();
            var winner = _game.CheckWhoWon(borad);
            switch (winner)
            {
                case GameResult.Draw:
                    GameInfo("Draw");
                    break;
                case GameResult.CirclePlayerWon:
                    GameInfo("Circle player won");
                    break;
                case GameResult.CrossPlayerWon:
                    GameInfo("Cross player won");
                    break;
            }
            return borad;
        }

        private void ExecuteNewGameAction()
        {
            
            foreach (var canvasType in _gameCanvases)
            {
                _canvasContainer.ClearCanvas(canvasType);
                UpdateCanvasView(canvasType);
            }
        }

        private void Draw(Point point, CanvasType canvasType)
        {
            _lastModifiedCanvas = canvasType;
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
