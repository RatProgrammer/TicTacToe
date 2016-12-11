using System;
using TicTacToe.Presenter;
using TicTacToe.View;

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
            TicTacToeForm ticTacToe = new TicTacToeForm();
            TicTacToePresenter ticTacToePresenter = new TicTacToePresenter(ticTacToe);
            ticTacToePresenter.RunApp();
        }
    }
}
