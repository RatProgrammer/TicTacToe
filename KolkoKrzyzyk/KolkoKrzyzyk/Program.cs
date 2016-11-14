using System;
using TicTacToe.Presenter;

namespace TicTacToe
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            View.TicTacToe ticTacToe = new View.TicTacToe();
            TicTacToePresenter ticTacToePresenter = new TicTacToePresenter(ticTacToe);
            ticTacToePresenter.RunApp();
        }
    }
}
