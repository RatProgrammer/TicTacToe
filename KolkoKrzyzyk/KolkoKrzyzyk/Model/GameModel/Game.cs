using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace TicTacToe.Model.GameModel
{
    public class Game
    {
        private GameUtil _gameUtil;

        public Game(GameUtil gameUtil)
        {
            _gameUtil = gameUtil;
        }

        public GameResult CheckWhoWon(int[] board)
        {
            var winningLines =_gameUtil.GetSumOfWinningLines(board);
            if (winningLines.Any(x => x == (int) GameResult.CrossPlayerWon))
            {
                return GameResult.CrossPlayerWon;
            }

            if (winningLines.Any(x => x == (int) GameResult.CirclePlayerWon))
            {
                return GameResult.CirclePlayerWon;
            }

            if (board.All(x => x > (int) GameResult.Draw))
            {
                return GameResult.Draw;
            }
            return GameResult.None;
        }
    }
}
