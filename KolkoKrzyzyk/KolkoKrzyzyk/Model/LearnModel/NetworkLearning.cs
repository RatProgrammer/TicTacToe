using System.Windows.Forms;
using AForge.Neuro;
using AForge.Neuro.Learning;
using TicTacToe.Model.DrawModel;
using TicTacToe.Model.NeuralModel;
using TicTacToe.View;

namespace TicTacToe.Model.LearnModel
{
    class NetworkLearning
    {
        public void Learn(LearningContainer learningContainer,LearningCanvas learningCanvas,ref ActivationNetwork network, ref BackPropagationLearning teacher)
        {
            learningContainer.SetChromaticImage(learningCanvas);
            var crossImage = learningContainer.GetChromaticImage(CanvasType.Cross);
            double[] crossInput = crossImage.GetChromaticImage();

            var circleImage = learningContainer.GetChromaticImage(CanvasType.Circle);
            double[] circleInput = circleImage.GetChromaticImage();


            var blankImage = learningContainer.GetChromaticImage(CanvasType.Blank);
            double[] blankInput = blankImage.GetChromaticImage();
           
            NetworkInput networkInput = new NetworkInput(crossInput,circleInput,blankInput);
            double [][] input = networkInput.Input;

            NetworkOutput networkOutput = new NetworkOutput(0);
            double[][] output = networkOutput.Output;

            for (int i = 0; i < 100000; i++)
            {
                double error = teacher.RunEpoch(input, output);
            }
            var result = network.Compute(input[9]);

            network.Save("Net.bin");
            network = (ActivationNetwork)ActivationNetwork.Load("Net.bin");
            var result1 = network.Compute(input[5]);
            MessageBox.Show("Skończona nauka");
        }
    }
}
