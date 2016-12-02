using System.Collections.Generic;
using System.Drawing;
using TicTacToe.View;

namespace TicTacToe.Model.DrawModel
{
    class DesignateCircle
    {
        public Bitmap DrawCircle(LearningCanvas learningCanvas, MyPen myPen)
        {
            Bitmap currentBitmap = myPen.DrawCircle(learningCanvas.GetCanvas(CanvasType.Result));
            learningCanvas.UpdateCanvas(currentBitmap, CanvasType.Circle);
            return currentBitmap;
        }
    }
}
