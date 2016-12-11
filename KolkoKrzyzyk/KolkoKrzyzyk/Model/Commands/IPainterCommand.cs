using System.Drawing;
using TicTacToe.Model.DrawModel;

namespace TicTacToe.Model.Commands
{
    internal interface IPainterCommand
    { 
        void Execute(ref Bitmap bitmap);
    }
}