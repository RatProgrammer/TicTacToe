using System;

namespace TicTacToe.Model.GameModel
{
    public class Game
    {
        private int[][] _boardWinningLines;
        private int[] _board;
        private int[] _lineStates;
        private int[] stan;

        /// <summary>
        /// Konstruktor argumentowy inicjacja lini w pozycjach wygranych
        /// </summary>
        /// <param name="board">Przekazywana tablica z aktualnym rozegraniem na planszy</param> 
        public Game(int[] board)
        {
            //linie wygrywające
            _boardWinningLines = new int[8][] 
            {   new int[] { 0, 1, 2 }, // 0
                new int[] { 3, 4, 5 }, // 1
                new int[] { 6, 7, 8 }, // 2
                new int[] { 0, 3, 6 }, // 3 
                new int[] { 1, 4, 7 }, // 4
                new int[] { 2, 5, 8 }, // 5
                new int[] { 0, 4, 8 }, // 6
                new int[] { 2, 4, 6 } }; //7
            _board = new int[9];
            //stan gry
            _board = board;
            _lineStates = new int[8];
            stan = new int[9];


        }
        /// <summary>
        /// Metoda wyznaczająca kolejny ruch komputera
        /// </summary>
        /// <returns></returns>
        public int CheckGame()
        {
            int sum = 0;
            for (int i = 0; i < _lineStates.Length; i++)
            {
                _lineStates[i] = 0;
                for (int j = 0; j < _boardWinningLines[i].Length; j++)
                {
                    sum += _board[_boardWinningLines[i][j]];
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
                        return -1;
                    case GameState.CoputerPlayerWin:
                        return -2;
                    default:
                        _lineStates[i] = 0;
                        break;
                }

                sum = 0;
            }

            this.stan[0] = _lineStates[0] + _lineStates[3] + _lineStates[6];
            this.stan[1] = _lineStates[0] + _lineStates[4];
            this.stan[2] = _lineStates[0] + _lineStates[5] + _lineStates[7];
            this.stan[3] = _lineStates[1] + _lineStates[3];
            this.stan[4] = _lineStates[4] + _lineStates[1] + _lineStates[6] + _lineStates[7];
            this.stan[5] = _lineStates[1] + _lineStates[5];
            this.stan[6] = _lineStates[2] + _lineStates[3] + _lineStates[7];
            this.stan[7] = _lineStates[2] + _lineStates[4];
            this.stan[8] = _lineStates[2] + _lineStates[5] + _lineStates[6];
            int max = -1;
            int field = -3;
            for (int i = 0; i < stan.Length; i++)
            {
                if (stan[i] > max && _board[i] == 0)
                {
                    max = stan[i];
                    field = i;
                }
            }
            return field;
        }
    }
}
