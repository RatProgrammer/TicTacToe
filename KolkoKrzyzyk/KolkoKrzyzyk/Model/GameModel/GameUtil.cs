using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe.Model.GameModel
{
    public class GameUtil
    {
        private int[] _lineStates;
        private readonly int[][] _boardWinningLines = new int[8][]
        {   new int[] { 0, 1, 2 }, // 0
            new int[] { 3, 4, 5 }, // 1
            new int[] { 6, 7, 8 }, // 2
            new int[] { 0, 3, 6 }, // 3 
            new int[] { 1, 4, 7 }, // 4
            new int[] { 2, 5, 8 }, // 5
            new int[] { 0, 4, 8 }, // 6
            new int[] { 2, 4, 6 } };

        public GameUtil()
        {
            _lineStates = new int[8];
        }


        public int[] GetSumOfWinningLines(int[] board)
        {
            int sum = 0;
            for (int i = 0; i < _lineStates.Length; i++)
            {
                _lineStates[i] = 0;
                for (int j = 0; j < _boardWinningLines[i].Length; j++)
                {
                    sum += board[_boardWinningLines[i][j]];
                }
                _lineStates[i] = sum;

                sum = 0;
            }
            return _lineStates;
        }

    }
}
