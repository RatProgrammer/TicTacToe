using System;
using System.Linq;
using TicTacToe.Model.Canvases;

namespace TicTacToe.Model.GameModel
{
    public class Game
    {
        private GameUtil _gameUtil;
        private GameBoard _gameBoard;
        private ComputerPlayer _computerPlayer;
        public Game(GameUtil gameUtil, GameBoard gameBoard, ComputerPlayer computerPlayer)
        {
            _gameUtil = gameUtil;
            _gameBoard = gameBoard;
            _computerPlayer = computerPlayer;
        }

        public GameResult CheckWhoWon()
        {
            var winningLines =_gameUtil.GetSumOfWinningLines(_gameBoard.GetBoardValues());
            if (winningLines.Any(x => x == (int) GameResult.CrossPlayerWon))
            {
                return GameResult.CrossPlayerWon;
            }

            if (winningLines.Any(x => x == (int) GameResult.CirclePlayerWon))
            {
                return GameResult.CirclePlayerWon;
            }

            if (_gameBoard.GetBoardValues().All(x => x > (int) GameResult.Draw))
            {
                return GameResult.Draw;
            }
            return GameResult.None;
        }

        public CanvasType ExecuteComputerPlayerMove()
        {
            var coordinate = _computerPlayer.PerformMove(_gameBoard);
            _gameBoard.UpdateBoard(coordinate, _computerPlayer.ComputerMark);
            return coordinate;
        }

        public GameMark[] GetBoard()
        {
            return _gameBoard.GetBoardValues();
        }

        public void UpdateBoard(CanvasType canvasType, GameMark gameMark)
        {
            _gameBoard.UpdateBoard(canvasType, gameMark);
        }

        public void ClearBoard()
        {
            _gameBoard.ClearBoard();
        }

        public GameMark GetComputerMark()
        {
            return _computerPlayer.ComputerMark;
        }

        public void SetComputerMark(GameMark humanMark)
        {
            switch (humanMark)
            {
                case GameMark.Cross:
                    _computerPlayer.ComputerMark = GameMark.Circle;
                    break;
                default:
                    _computerPlayer.ComputerMark = GameMark.Cross;
                    break;
            }
        }
    }
}
