using System.Drawing;

namespace TicTacToe.Model.Commands
{
    class NullObjectCommand : IPainterCommand
    {
        public void Execute(ref Bitmap bitmap)
        {
            
        }
    }
}
