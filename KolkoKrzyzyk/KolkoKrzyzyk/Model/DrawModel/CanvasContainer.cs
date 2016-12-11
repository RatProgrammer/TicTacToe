using System;
using System.Collections.Generic;
using TicTacToe.Model.Canvases;
using TicTacToe.Model.Commands;

namespace TicTacToe.Model.DrawModel
{
    class CanvasContainer
    {
        private Dictionary<CanvasType, Canvas> _canvases;

        public CanvasContainer()
        {
            _canvases = new Dictionary<CanvasType, Canvas>()
            {
                {CanvasType.Circle, new Canvas()},
                {CanvasType.Cross, new Canvas()},
                {CanvasType.Blank, new Canvas()},
                {CanvasType.Result, new Canvas()},
                {CanvasType.Test, new Canvas()},
                {CanvasType.Matrix11, new Canvas()},
                {CanvasType.Matrix12, new Canvas()},
                {CanvasType.Matrix13, new Canvas()},
                {CanvasType.Matrix21, new Canvas()},
                {CanvasType.Matrix22, new Canvas()},
                {CanvasType.Matrix23, new Canvas()},
                {CanvasType.Matrix31, new Canvas()},
                {CanvasType.Matrix32, new Canvas()},
                {CanvasType.Matrix33, new Canvas()}
            };
        }

        public void UpdateCanvas(Canvas canvas, CanvasType canvasType)
        {
            _canvases[canvasType].CopyCanvas(canvas);
        }

        public Canvas GetCanvas(CanvasType canvasType)
        {
            return _canvases[canvasType];
        }

        public void ClearCanvas(CanvasType canvasType)
        {
            var canvas = GetCanvas(canvasType);
            canvas.ClearCanvas();
        }

        public void DrawOnCanvas(CanvasType canvasType, IPainterCommand command)
        {
            _canvases[canvasType].DrawOnCanvas(command);
        }
    }
}
