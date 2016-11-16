using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var bitmapToTest = learningContainer.GetChromaticImage(CanvasType.pcTest);
            double[] testInput = bitmapToTest.GetChromaticImage();
            network = (ActivationNetwork)ActivationNetwork.Load("Net.bin");
            teacher = new BackPropagationLearning((ActivationNetwork)network);
            double[] netout = network.Compute(testInput);
            if (netout[0] > 0.6 && netout[1] < 0.1)
            {
                DesignateCross designateCross = new DesignateCross();
                List<Point> points = designateCross.Designate();
                _currentBitmap = myPen.DrawShape(points, learningCanvas.GetCanvas(CanvasType.pcCross));
                learningCanvas.UpdateCanvas(_currentBitmap, CanvasType.pcResult);
                MessageBox.Show("Krzyżyk");
                MessageBox.Show("Skończony test");
                return _currentBitmap;

            }
            else if (netout[0] < 0.1 && netout[1] > 0.6)
            {
                DesignateCircle designateCircle = new DesignateCircle();
                List<Point> points = designateCircle.Designate();
                _currentBitmap = myPen.DrawShape(points, learningCanvas.GetCanvas(CanvasType.pcCircle));
                learningCanvas.UpdateCanvas(_currentBitmap, CanvasType.pcResult);
                MessageBox.Show("Kółko");
                MessageBox.Show("Skończony test");
                return _currentBitmap;
            }
            else
            {
                MessageBox.Show("Pusty");
                MessageBox.Show("Skończony test");
                return _currentBitmap;


            }
            MessageBox.Show("Skończony test");
        }
    }
}
