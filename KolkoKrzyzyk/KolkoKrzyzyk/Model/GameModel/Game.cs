using System;

namespace TicTacToe.Model.GameModel
{
    public class Game
    {
        private int[][] _boardWinningLines;
        private int[] _lineStates;
        private int[] _state;
        public event Action HumanPlayerWinAction;
        public event Action ComputerPlayerWinAction;
        public event Action DrawAction;

        public Game()
        {
            _boardWinningLines = new int[8][] 
            {   new int[] { 0, 1, 2 }, // 0
                new int[] { 3, 4, 5 }, // 1
                new int[] { 6, 7, 8 }, // 2
                new int[] { 0, 3, 6 }, // 3 
                new int[] { 1, 4, 7 }, // 4
                new int[] { 2, 5, 8 }, // 5
                new int[] { 0, 4, 8 }, // 6
                new int[] { 2, 4, 6 } }; //7
            _lineStates = new int[8];
            _state = new int[9];

        }

        public int CheckGame(int[] board)
        {
            int sum = 0;
            for (int i = 0; i < _lineStates.Length; i++)
            {
                _lineStates[i] = 0;
                for (int j = 0; j < _boardWinningLines[i].Length; j++)
                {
                    sum += board[_boardWinningLines[i][j]];
                }
                switch ((GameState)sum)
                {
                    case GameState.Danger:
                        _lineStates[i] = 20;
                        break;
                    case GameState.Chance:
                        _lineStates[i] = 10000;
                        break;
                    case GameState.LittleChance:
                        _lineStates[i] = 5;
                        break;
                    case GameState.LittleDanger:
                        _lineStates[i] = 2;
                        break;
                    case GameState.VerySmallChance:
                        _lineStates[i] = 1;
                        break;
                    case GameState.HumanPlayerWin:
                        HumanPlayerWinAction?.Invoke();
                        return -1;
                    case GameState.ComputerPlayerWin:
                        ComputerPlayerWinAction?.Invoke();
                        return -2;
                    default:
                        _lineStates[i] = 0;
                        break;
                }

                sum = 0;
            }

            _state[0] = _lineStates[0] + _lineStates[3] + _lineStates[6];
            _state[1] = _lineStates[0] + _lineStates[4];
            _state[2] = _lineStates[0] + _lineStates[5] + _lineStates[7];
            _state[3] = _lineStates[1] + _lineStates[3];
            _state[4] = _lineStates[4] + _lineStates[1] + _lineStates[6] + _lineStates[7];
            _state[5] = _lineStates[1] + _lineStates[5];
            _state[6] = _lineStates[2] + _lineStates[3] + _lineStates[7];
            _state[7] = _lineStates[2] + _lineStates[4];
            _state[8] = _lineStates[2] + _lineStates[5] + _lineStates[6];
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

            if (field == -3)
            {
                DrawAction?.Invoke();
            }
            return field;
        }
    }
}
