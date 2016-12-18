namespace TicTacToe.Model.NeuralModel
{
    public struct NetworkOutput
    {
        public double[][] Output;

        public NetworkOutput(int a)
        {
            Output = new double[22][]
            {
                //8 cross 8 circle 6 blank
                new double[] {1, 0}, //cross
                new double[] {0, 1}, //circle
                new double[] {0, 0}, //blank
                new double[] {0, 0}, //blank
                new double[] {0, 1}, //circle
                new double[] {0, 1}, //circle
                new double[] {1, 0}, //cross
                new double[] {1, 0}, //cross
                new double[] {1, 0}, //cross
                new double[] {0, 0}, //blank
                new double[] {0, 1}, //circle
                new double[] {0, 1}, //circle
                new double[] {0, 1}, //circle
                new double[] {1, 0}, //cross
                new double[] {0, 1}, //circle
                new double[] {0, 1}, //circle
                new double[] {1, 0}, //cross
                new double[] {1, 0}, //cross
                new double[] {1, 0}, //cross
                new double[] {0, 1}, //circle
                new double[] {0, 1}, //circle
                new double[] {1, 0}, //cross
            };
        }
    }
}