using System.Windows.Forms;
using AForge.Neuro;
using AForge.Neuro.Learning;
using TicTacToe.Model.NeuralModel;

namespace TicTacToe.Model.LearnModel
{
    class NetworkLearning
    {

        public void Learn(ref ActivationNetwork network, double[] crossInput, double[] circleInput, double[] blankInput)
        {
            NetworkInput networkInput = new NetworkInput(crossInput, circleInput, blankInput);
            double[][] input = networkInput.Input;

            NetworkOutput networkOutput = new NetworkOutput(0);

            double[][] output = networkOutput.Output;

            BackPropagationLearning teacher =
                new BackPropagationLearning(network);

            for (int i = 0; i < 100000; i++)
            {
                double error = teacher.RunEpoch(input, output);
            }

            network.Save("Net.bin");
            MessageBox.Show("Learning finished");
        }
    }
}
