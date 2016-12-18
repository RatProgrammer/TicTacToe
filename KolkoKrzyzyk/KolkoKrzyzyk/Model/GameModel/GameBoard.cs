using System.Collections.Generic;
using System.Linq;
using TicTacToe.Model.Canvases;

namespace TicTacToe.Model.GameModel
{
    public class GameBoard
    {
        private Dictionary<CanvasType, GameMark> _board;

        public GameBoard(List<CanvasType> matrix)
        {
            _board = new Dictionary<CanvasType, GameMark>();
            foreach (var canvasType in matrix)
            {
                _board.Add(canvasType, GameMark.Blank);
            }
        }

        public void UpdateBoard(CanvasType canvasType, GameMark gameMark)
        {
            _board[canvasType] = gameMark;
        }

        public GameMark[] GetBoardValues()
        {
            return _board.Values.ToArray();
        }

        public void ClearBoard()
        {
            foreach (var source in _board.Keys.ToList())
            {
                _board[source] = GameMark.Blank;
            }
        }
    }
}
