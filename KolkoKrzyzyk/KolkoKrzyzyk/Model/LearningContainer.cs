using System;
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
            _chromaticImageCross = new ChromaticImage(Convert(CanvasType.pcCross, learningCanvas));
            _chromaticImageCircle = new ChromaticImage(Convert(CanvasType.pcCircle, learningCanvas));
            _chromaticImageBlank = new ChromaticImage(Convert(CanvasType.pcBlank, learningCanvas));
            _chromaticImageTest = new ChromaticImage(Convert(CanvasType.pcTest, learningCanvas));
        }

        public ChromaticImage GetChromaticImage(CanvasType canvasType)
        {
            switch (canvasType)
            {
                case CanvasType.pcCross:
                    return _chromaticImageCross;
                case CanvasType.pcCircle:
                    return _chromaticImageCircle;
                case CanvasType.pcBlank:
                    return _chromaticImageBlank;
                case CanvasType.pcTest:
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
