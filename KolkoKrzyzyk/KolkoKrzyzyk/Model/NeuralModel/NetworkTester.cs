using System.Drawing;
using System.Windows.Forms;
using AForge.Neuro;
using AForge.Neuro.Learning;
using TicTacToe.Model.Canvases;
using TicTacToe.Model.DrawModel;
using TicTacToe.View;

namespace TicTacToe.Model.NeuralModel
{
    class NetworkTester
    {
        private ActivationNetwork _network;

        public NetworkTester(ActivationNetwork network)
        {
            _network = network;
        }

        public  GameMark Test(double [] testInput)
        {
            double[] netout = _network.Compute(testInput);

            if (netout[0] > 0.6 && netout[1] < 0.1)
            {
                return GameMark.Cross;
            }
            else if (netout[0] < 0.1 && netout[1] > 0.6)
            {
                return GameMark.Circle;
            }
            else
            {
                return GameMark.Blank;
            }
        }
    }
}
