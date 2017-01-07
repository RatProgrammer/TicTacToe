using System.Collections.Generic;
using System.Drawing;
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
        private GameBoard _board;
        private PaintFactory _paintFactory;
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
            _board = new GameBoard(_gameCanvases);
            _game = new Game(_gameUtil, _board, _computerPlayer);
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
            _lastModifiedCanvas = CanvasType.Matrix11;
            _paintFactory = new PaintFactory(_pen, _crossDesignator);
        }

        private void ExecuteBackAction()
        {
            _canvasContainer.ClearCanvas(_lastModifiedCanvas);
            UpdateCanvasView(_lastModifiedCanvas);
        }

        private void GameInfo(string message)
        {
            _ticTacToeForm.ShowMessage(message);
        }

        private void ExecutePlayAction()
        {
            var result = PrepareBoard();
            if (result)
            {
                CheckGame();
                var move = _game.ExecuteComputerPlayerMove();
                var computerMark = _game.GetComputerMark();
                _canvasContainer.DrawOnCanvas(move, _paintFactory.GetPainter(computerMark));
                UpdateCanvasView(move);
                CheckGame();
            }
        }

        private bool PrepareBoard()
        {
            var humanCanvas = _lastModifiedCanvas;
            NetworkTester networkTester = new NetworkTester(_network);
            var canvas = _canvasContainer.GetCanvas(humanCanvas);
            var humanMark = networkTester.Test(BitmapConverter.ImageToByte(canvas.GetBitmap()));
            if (humanMark == GameMark.Blank)
            {
                GameInfo("Try again");
                _canvasContainer.ClearCanvas(humanCanvas);
                UpdateCanvasView(humanCanvas);
                return false;
            }
                _game.SetComputerMark(humanMark);

                _game.UpdateBoard(humanCanvas, humanMark);
            return true;

        }

        private void CheckGame()
        {
            var winner = _game.CheckWhoWon();

            switch (winner)
            {
                case GameResult.Draw:
                    GameInfo("Draw");
                    ExecuteNewGameAction();
                    break;
                case GameResult.CirclePlayerWon:
                    GameInfo("Circle player won");
                    ExecuteNewGameAction();
                    break;
                case GameResult.CrossPlayerWon:
                    GameInfo("Cross player won");
                    ExecuteNewGameAction();
                    break;
            }
        }

        private void ExecuteNewGameAction()
        {
            foreach (var canvasType in _gameCanvases)
            {
                _canvasContainer.ClearCanvas(canvasType);
                UpdateCanvasView(canvasType);
            }
            _lastModifiedCanvas = CanvasType.Matrix11;
            _game.ClearBoard();
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
            NetworkTester networkTester = new NetworkTester(_network);
            var inputData = PreaperNetworkInput(CanvasType.Test);
            GameMark gameMark = networkTester.Test(inputData);
            if (gameMark == GameMark.Circle)
            {
                _painterCommand = new DrawCircleCommand(_pen);
                _canvasContainer.DrawOnCanvas(CanvasType.Result, _painterCommand);
                GameInfo("Circle");
            }
            if (gameMark == GameMark.Cross)
            {
                _painterCommand = new DrawCrossCommand(_pen,_crossDesignator);
                _canvasContainer.DrawOnCanvas(CanvasType.Result, _painterCommand);
                GameInfo("Cross");
            }
            else
            {
                GameInfo("Blank");
            }
            UpdateCanvasView(CanvasType.Result);
        }

        private void ExecuteLearnAction()
        { 
            _ticTacToeForm.UpdateInformationLabel("Learning in progress. \n Please wait.");
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
