using System.Collections.Generic;
using TicTacToe.Model.Canvases;

namespace TicTacToe.Model.GameModel
{
    class ComputerPlayer
    {
        private GameUtil _gameUtil;
        private List<CanvasType> _canvasTypes;
        private int[] _state;

        public ComputerPlayer(GameUtil gameUtil, List<CanvasType> canvasTypes)
        {
            _gameUtil = gameUtil;
            _canvasTypes = canvasTypes;
            _state = new int[9];
        }

        public CanvasType PerformMove(int[] board)
        {
            var winningProbability = _gameUtil.GetSumOfWinningLines(board);
            for (int i = 0; i < winningProbability.Length; i++)
            {
                switch ((GameState)winningProbability[i])
                {
                    case GameState.Danger:
                        winningProbability[i] = 20;
                        break;
                    case GameState.Chance:
                        winningProbability[i] = 10000;
                        break;
                    case GameState.LittleChance:
                        winningProbability[i] = 5;
                        break;
                    case GameState.LittleDanger:
                        winningProbability[i] = 2;
                        break;
                    case GameState.VerySmallChance:
                        winningProbability[i] = 1;
                        break;
                    default:
                        winningProbability[i] = 0;
                        break;
                }
            }

            _state[0] = winningProbability[0] + winningProbability[3] + winningProbability[6];
            _state[1] = winningProbability[0] + winningProbability[4];
            _state[2] = winningProbability[0] + winningProbability[5] + winningProbability[7];
            _state[3] = winningProbability[1] + winningProbability[3];
            _state[4] = winningProbability[4] + winningProbability[1] + winningProbability[6] + winningProbability[7];
            _state[5] = winningProbability[1] + winningProbability[5];
            _state[6] = winningProbability[2] + winningProbability[3] + winningProbability[7];
            _state[7] = winningProbability[2] + winningProbability[4];
            _state[8] = winningProbability[2] + winningProbability[5] + winningProbability[6];
            int max = -1;
            int field = -3;
            for (int i = 0; i < _state.Length; i++)
            {
                if (_state[i] > max && board[i] == 0)
                {
                    max = _state[i];
                    field = i;
                }
            }
            return _canvasTypes[field];
        }
    }
}
