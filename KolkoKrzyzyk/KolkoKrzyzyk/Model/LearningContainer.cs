using System;
using System.Drawing;
using TicTacToe.Model.DrawModel;
using TicTacToe.View;

namespace TicTacToe.Model
{
    class LearningContainer
    {
        private ChromaticImage _chromaticImageCross;
        private ChromaticImage _chromaticImageCircle;
        private ChromaticImage _chromaticImageBlank;
        private ChromaticImage _chromaticImageTest;

        public void SetChromaticImage(LearningCanvas learningCanvas)
        {
            _chromaticImageCross = new ChromaticImage(Convert(CanvasType.Cross, learningCanvas));
            _chromaticImageCircle = new ChromaticImage(Convert(CanvasType.Circle, learningCanvas));
            _chromaticImageBlank = new ChromaticImage(Convert(CanvasType.Blank, learningCanvas));
            _chromaticImageTest = new ChromaticImage(Convert(CanvasType.Test, learningCanvas));
        }

        public ChromaticImage GetChromaticImage(CanvasType canvasType)
        {
            switch (canvasType)
            {
                case CanvasType.Cross:
                    return _chromaticImageCross;
                case CanvasType.Circle:
                    return _chromaticImageCircle;
                case CanvasType.Blank:
                    return _chromaticImageBlank;
                case CanvasType.Test:
                    return _chromaticImageTest;
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }

        private double[] Convert(CanvasType canvasType,LearningCanvas learningCanvas)
        {
           return  BitmapConverter.ImageToByte(learningCanvas.GetCanvas(canvasType));
        }
    }
}
