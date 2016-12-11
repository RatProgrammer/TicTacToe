using System;
using System.Collections.Generic;
using TicTacToe.Model.Canvases;
using TicTacToe.Model.Commands;

namespace TicTacToe.Model.DrawModel
{
    class LearnCanvasContainer
    {
        private Dictionary<LearnCanvasType, Canvas> _canvases;

        public LearnCanvasContainer()
        {
            _canvases = new Dictionary<LearnCanvasType, Canvas>()
            {
                {LearnCanvasType.Circle, new Canvas()},
                {LearnCanvasType.Cross, new Canvas()},
                {LearnCanvasType.Blank, new Canvas()},
                {LearnCanvasType.Result, new Canvas()},
                {LearnCanvasType.Test, new Canvas()}
            };
        }

        public void UpdateCanvas(Canvas canvas, LearnCanvasType learnCanvasType)
        {
            _canvases[learnCanvasType].CopyCanvas(canvas);
        }

        public Canvas GetCanvas(LearnCanvasType learnCanvasType)
        {
            return _canvases[learnCanvasType];
        }

        public void ClearCanvas(LearnCanvasType learnCanvasType)
        {
            var canvas = GetCanvas(learnCanvasType);
            canvas.ClearCanvas();
        }

        public void DrawOnCanvas(LearnCanvasType canvasType, IPainterCommand command)
        {
            _canvases[canvasType].DrawOnCanvas(command);
        }
    }
}
