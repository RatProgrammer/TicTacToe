using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KolkoKrzyzyk.Presenter;

namespace KolkoKrzyzyk
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TicTacToe ticTacToe = new TicTacToe();
            TicTacToePresenter ticTacToePresenter = new TicTacToePresenter(ticTacToe);
            ticTacToePresenter.RunApp();
        }
    }
}
