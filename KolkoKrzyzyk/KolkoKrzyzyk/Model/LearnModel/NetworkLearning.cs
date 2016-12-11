using System.Windows.Forms;
using AForge.Neuro;
using AForge.Neuro.Learning;
using TicTacToe.Model.Canvases;
using TicTacToe.Model.DrawModel;
using TicTacToe.Model.NeuralModel;
using TicTacToe.View;

namespace TicTacToe.Model.LearnModel
{
    class NetworkLearning
    {
        private BackPropagationLearning _teacher;
        private Network _network;

        public NetworkLearning(BackPropagationLearning teacher, Network network)
        {
            _teacher = teacher;
            _network = network;
        }

        public void Learn(double[] crossInput, double[] circleInput, double[] blankInput)
        {
            NetworkInput networkInput = new NetworkInput(crossInput, circleInput, blankInput);
            double[][] input = networkInput.Input;

            NetworkOutput networkOutput = new NetworkOutput(0);
            double[][] output = networkOutput.Output;

            for (int i = 0; i < 100000; i++)
            {
                double error = _teacher.RunEpoch(input, output);
            }
            var result = _network.Compute(input[9]);

            _network.Save("Net.bin");
            _network = (ActivationNetwork)Network.Load("Net.bin");
            var result1 = _network.Compute(input[5]);
            MessageBox.Show("Skończona nauka");
        }
    }
}
