using System.Drawing;
using System.Windows.Forms;
using AForge.Neuro;
using AForge.Neuro.Learning;
using TicTacToe.Model;
using TicTacToe.Model.Canvases;
using TicTacToe.Model.Commands;
using TicTacToe.Model.DrawModel;
using TicTacToe.Model.LearnModel;
using TicTacToe.Model.NeuralModel;
using TicTacToe.View;

namespace TicTacToe.Presenter
{
    class TicTacToePresenter
    {
        private readonly TicTacToeForm _ticTacToeForm;
        private  readonly LearnCanvasContainer _learnCanvasContainer;
        private ActivationNetwork _network;
        private BackPropagationLearning _teacher;               
        private IPainterCommand _painterCommand;
        private Pen _pen;
        private CrossDesignator _crossDesignator;
        public TicTacToePresenter(TicTacToeForm ticTacToeForm)
        {
            _ticTacToeForm = ticTacToeForm;
            _learnCanvasContainer = new LearnCanvasContainer();
            _network = new ActivationNetwork(new SigmoidFunction(2), 100, 14, 2);
            _teacher = new BackPropagationLearning(_network);
            _pen = new Pen(Color.Black, 5);
            _crossDesignator = new CrossDesignator();

            _ticTacToeForm.DrawLearnWindowAction += DrawOnLearnWindow;
            _ticTacToeForm.LearnAction += ExecuteLearnAction;
            _ticTacToeForm.CrossAction += ExecuteCrossAction;
            _ticTacToeForm.CircleAction += ExecuteCircleAction;
            _ticTacToeForm.ClearAction += ExecuteClearAction;
            _ticTacToeForm.CopyAction += ExecuteCopyAction;
            _ticTacToeForm.TestAction += ExecuteTestAction;
        }

        private void DrawOnLearnWindow(Point point, LearnCanvasType learnCanvasType)
        {
            _painterCommand = new DrawPointCommand(point, _pen);
            _learnCanvasContainer.DrawOnCanvas(learnCanvasType, _painterCommand);
            UpdateCanvasView(learnCanvasType);
        }

        private void ExecuteTestAction()
        {
            _learnCanvasContainer.ClearCanvas(LearnCanvasType.Result);
            LoadNetworkFromFile();
            NetworkTester networkTester = new NetworkTester(_network);
            var inputData = PreaperNetworkInput(LearnCanvasType.Test);
            GameMark gameMark = networkTester.Test(inputData);
            if (gameMark == GameMark.Circle)
            {
                _painterCommand = new DrawCircleCommand(_pen);
                _learnCanvasContainer.DrawOnCanvas(LearnCanvasType.Result, _painterCommand);
            }
            if (gameMark == GameMark.Cross)
            {
                _painterCommand = new DrawCrossCommand(_pen,_crossDesignator);
                _learnCanvasContainer.DrawOnCanvas(LearnCanvasType.Result, _painterCommand);
            }
            UpdateCanvasView(LearnCanvasType.Result);
        }

        private void ExecuteLearnAction()
        { 
            _ticTacToeForm.ShowMessage("Trwa nauka. \n Proszę czekać.");
            LoadNetworkFromFile();
            NetworkLearning networkLearning = new NetworkLearning(_teacher, _network);
            var crossInput = PreaperNetworkInput(LearnCanvasType.Cross);
            var circleInput = PreaperNetworkInput(LearnCanvasType.Circle);
            var blankInput = PreaperNetworkInput(LearnCanvasType.Blank);
            networkLearning.Learn(crossInput, circleInput, blankInput);
            _ticTacToeForm.ShowMessage("");
        }

        private double[] PreaperNetworkInput(LearnCanvasType canvasType)
        {
            var canvas = _learnCanvasContainer.GetCanvas(canvasType);
            var input = BitmapConverter.ImageToByte(canvas.GetBitmap());
            return input;
        }

        private void ExecuteClearAction(LearnCanvasType learnCanvasType)
        {
            _learnCanvasContainer.ClearCanvas(learnCanvasType);
            UpdateCanvasView(learnCanvasType);
        }
        private void ExecuteCopyAction(LearnCanvasType learnCanvasType)
        {
            var currentBitmap = _learnCanvasContainer.GetCanvas(LearnCanvasType.Test);
            _learnCanvasContainer.UpdateCanvas(currentBitmap, learnCanvasType);
            UpdateCanvasView(learnCanvasType);
        }

        private void ExecuteCrossAction()
        {
            _painterCommand = new DrawCrossCommand(_pen, _crossDesignator );
            _learnCanvasContainer.DrawOnCanvas(LearnCanvasType.Cross, _painterCommand);
            UpdateCanvasView(LearnCanvasType.Cross);
        }
        private void ExecuteCircleAction()
        {
            _painterCommand = new DrawCircleCommand(_pen);
            _learnCanvasContainer.DrawOnCanvas(LearnCanvasType.Circle, _painterCommand);
            UpdateCanvasView(LearnCanvasType.Circle);
        }
        public void RunApp()
        {
            Application.EnableVisualStyles();
            Application.Run(_ticTacToeForm);
        }

        public void UpdateCanvasView(LearnCanvasType learnCanvasType)
        {
            var canvas = _learnCanvasContainer.GetCanvas(learnCanvasType);
            var bitmap = canvas.GetBitmap();
            switch (learnCanvasType)
            {
                case LearnCanvasType.Cross:
                    _ticTacToeForm.UpdateCanvasCross(bitmap);
                    break;
                case LearnCanvasType.Circle:
                    _ticTacToeForm.UpdateCanvasCircle(bitmap);
                    break;
                case LearnCanvasType.Blank:
                    _ticTacToeForm.UpdateCanvasBlank(bitmap);
                    break;
                case LearnCanvasType.Test:
                    _ticTacToeForm.UpdateCanvasTest(bitmap);
                    break;
                case LearnCanvasType.Result:
                    _ticTacToeForm.UpdateCanvasResult(bitmap);
                    break;
            }
        }

        private void LoadNetworkFromFile()
        {
            _network = (ActivationNetwork)Network.Load("Net.bin");
        }

    }
}
