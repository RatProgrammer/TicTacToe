using System.Drawing;
using System.Windows.Forms;
using AForge.Neuro;
using AForge.Neuro.Learning;
using TicTacToe.Model.DrawModel;
using TicTacToe.View;

namespace TicTacToe.Model.NeuralModel
{
    class NetworkTesting
    {
        private Bitmap _currentBitmap;
        public  Bitmap Test(ref LearningContainer learningContainer,LearningCanvas learningCanvas, ref ActivationNetwork network, MyPen myPen, ref BackPropagationLearning teacher)
        {
            learningContainer.SetChromaticImage(learningCanvas);
            var bitmapToTest = learningContainer.GetChromaticImage(CanvasType.Test);
            double[] testInput = bitmapToTest.GetChromaticImage();
            network = (ActivationNetwork)ActivationNetwork.Load("Net.bin");
            teacher = new BackPropagationLearning((ActivationNetwork)network);
            double[] netout = network.Compute(testInput);

            if (netout[0] > 0.6 && netout[1] < 0.1)
            {
                DesignateCross designateCross = new DesignateCross();
                _currentBitmap = designateCross.DrawCross(learningCanvas, myPen);
                MessageBox.Show("Krzyżyk");
                return _currentBitmap;

            }
            else if (netout[0] < 0.1 && netout[1] > 0.6)
            {
                DesignateCircle designateCircle = new DesignateCircle();
                _currentBitmap = designateCircle.DrawCircle(learningCanvas, myPen);
                MessageBox.Show("Kółko");
                return _currentBitmap;
            }
            else
            {
                MessageBox.Show("Pusty");
                return _currentBitmap;
            }
        }
    }
}
