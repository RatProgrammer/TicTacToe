﻿namespace TicTacToe.Model.NeuralModel
{
    public struct NetworkInput
    {
        public double[][] Input;

        public NetworkInput(double[] crossInput, double[] circleInput, double[] blankInput)
        {
            Input = new double[22][]
            {
                crossInput,
                circleInput,
                blankInput,
                new double[]
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //blank
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                },
                new double[]
                {
                    0, 0, 0, 1, 1, 1, 0, 0, 0, 0, //circle
                    0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
                    0, 0, 1, 0, 0, 0, 0, 1, 0, 0,
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
                    0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
                    0, 0, 0, 1, 1, 1, 0, 0, 0, 0
                },

                new double[]
                {
                    0, 0, 0, 0, 1, 1, 0, 0, 0, 0, //circle
                    0, 0, 0, 1, 0, 0, 1, 0, 0, 0,
                    0, 0, 1, 0, 0, 0, 0, 1, 0, 0,
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
                    0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
                    0, 0, 0, 1, 1, 1, 0, 0, 0, 0
                },
                new double[]
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //cross
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 0, 1, 0, 0, 0, 0, 1, 0, 0,
                    0, 0, 0, 1, 0, 0, 1, 0, 0, 0,
                    0, 0, 0, 0, 1, 1, 0, 0, 0, 0,
                    0, 0, 0, 0, 1, 1, 0, 0, 0, 0,
                    0, 0, 0, 1, 0, 0, 1, 0, 0, 0,
                    0, 0, 1, 0, 0, 0, 0, 1, 0, 0,
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                },
                new double[]
                {
                    1, 1, 0, 0, 0, 0, 0, 0, 0, 0, //cross
                    0, 1, 1, 0, 0, 0, 0, 0, 1, 0,
                    0, 0, 1, 1, 0, 0, 0, 1, 0, 0,
                    0, 0, 0, 1, 1, 0, 1, 0, 0, 0,
                    0, 0, 0, 0, 1, 1, 0, 0, 0, 0,
                    0, 0, 0, 0, 1, 1, 1, 0, 0, 0,
                    0, 0, 0, 1, 0, 0, 1, 1, 0, 0,
                    0, 0, 1, 0, 0, 0, 0, 1, 1, 0,
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 1,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 1
                },
                new double[]
                {
                    1, 0, 0, 0, 0, 0, 0, 0, 0, 0, //cross
                    1, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 1, 1, 0, 0, 0, 0, 1, 0, 0,
                    0, 0, 1, 1, 0, 0, 1, 0, 0, 0,
                    0, 0, 0, 1, 1, 1, 0, 0, 0, 0,
                    0, 0, 0, 0, 1, 1, 0, 0, 0, 0,
                    0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
                    0, 0, 1, 0, 0, 0, 1, 1, 0, 0,
                    0, 1, 0, 0, 0, 0, 0, 1, 1, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 1, 1
                },
                //new double[]
                //{
                //    1, 1, 1, 1, 1, 1, 1, 1, 1, 1, //blank
                //    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                //    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                //    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                //    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                //    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                //    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                //    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                //    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                //    1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                //},
                new double[]
                {
                    1, 1, 1, 1, 1, 1, 1, 1, 1, 1, //blank
                    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                },
                //new double[]
                //{
                //    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //blank
                //    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                //    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                //    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                //    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                //    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                //    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                //    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                //    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                //    0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                //},
                new double[]
                {
                    0, 0, 0, 1, 1, 1, 0, 0, 0, 0, //circle
                    0, 0, 1, 1, 1, 1, 1, 0, 0, 0,
                    0, 1, 1, 0, 0, 0, 0, 1, 0, 0,
                    1, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    1, 1, 0, 0, 0, 0, 0, 0, 1, 1,
                    1, 1, 0, 0, 0, 0, 0, 0, 1, 1,
                    1, 1, 0, 0, 0, 0, 0, 0, 1, 1,
                    0, 1, 0, 0, 0, 0, 0, 1, 1, 0,
                    0, 1, 1, 0, 0, 1, 1, 1, 0, 0,
                    0, 0, 1, 1, 1, 1, 1, 0, 0, 0
                },
                new double[]
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //circle
                    0, 0, 1, 1, 1, 1, 1, 0, 0, 0,
                    0, 1, 1, 0, 0, 0, 0, 1, 0, 0,
                    1, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                    1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                    1, 1, 0, 0, 0, 0, 0, 0, 0, 1,
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 1, 0, 0, 0, 0, 1, 1, 0, 0,
                    0, 0, 1, 1, 1, 1, 1, 0, 0, 0
                },
                new double[]
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //circle
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
                    0, 0, 1, 0, 0, 0, 0, 1, 0, 0,
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
                    0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
                    0, 0, 0, 1, 1, 1, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                },
                new double[]
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //cross
                    0, 0, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 0, 1, 0, 0, 0, 0, 1, 1, 0,
                    0, 0, 0, 1, 0, 0, 1, 1, 0, 0,
                    0, 0, 0, 1, 1, 1, 0, 0, 0, 0,
                    0, 0, 0, 0, 1, 1, 1, 0, 0, 0,
                    0, 0, 0, 1, 0, 0, 1, 0, 0, 0,
                    0, 0, 1, 0, 0, 0, 1, 1, 0, 0,
                    0, 1, 1, 0, 0, 0, 0, 1, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                },
                new double[]
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //circle
                    0, 0, 0, 1, 1, 1, 0, 0, 0, 0,
                    0, 0, 1, 0, 1, 1, 1, 0, 0, 0,
                    0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
                    0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
                    0, 0, 0, 1, 1, 1, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                },
                new double[]
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //circle
                    0, 0, 1, 1, 1, 1, 0, 0, 0, 0,
                    0, 1, 1, 1, 1, 1, 1, 0, 0, 0,
                    0, 1, 1, 1, 0, 1, 0, 0, 0, 0,
                    0, 1, 1, 0, 0, 1, 1, 0, 0, 0,
                    1, 1, 1, 0, 0, 1, 1, 0, 0, 0,
                    1, 1, 1, 0, 1, 1, 0, 0, 0, 0,
                    1, 1, 1, 1, 1, 1, 0, 0, 0, 0,
                    0, 1, 1, 1, 1, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                },
                new double[]
                {
                    1, 0, 0, 0, 0, 0, 0, 0, 0, 0, //cross
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 0, 1, 0, 0, 0, 0, 1, 1, 0,
                    0, 0, 0, 1, 0, 0, 1, 1, 0, 0,
                    0, 0, 0, 1, 1, 1, 0, 0, 0, 0,
                    0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
                    0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
                    0, 1, 0, 0, 0, 0, 1, 1, 0, 0,
                    1, 0, 0, 0, 0, 0, 0, 1, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 1, 0
                },
                new double[]
                {
                    1, 0, 0, 0, 0, 0, 0, 0, 0, 0, //cross
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    0, 0, 1, 0, 0, 0, 0, 1, 1, 0,
                    0, 0, 0, 1, 0, 0, 1, 1, 0, 0,
                    0, 0, 0, 1, 1, 1, 0, 0, 0, 0,
                    0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
                    0, 0, 1, 0, 0, 0, 0, 1, 0, 0,
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                },
                new double[]
                {
                    1, 0, 0, 0, 0, 0, 0, 0, 0, 0, //cross
                    0, 1, 0, 0, 0, 0, 0, 0, 0, 1,
                    0, 0, 1, 0, 0, 0, 0, 0, 1, 0,
                    0, 0, 0, 1, 0, 0, 0, 1, 0, 0,
                    0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
                    0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
                    0, 0, 1, 0, 0, 0, 0, 1, 0, 0,
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                },
                new double[]
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //circle
                    0, 0, 1, 1, 1, 1, 0, 0, 0, 0,
                    0, 1, 1, 0, 1, 1, 1, 0, 0, 0,
                    0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
                    0, 1, 0, 0, 0, 0, 1, 1, 1, 0,
                    0, 1, 0, 0, 0, 0, 1, 1, 0, 0,
                    0, 0, 1, 0, 0, 0, 1, 1, 0, 0,
                    0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
                    0, 0, 0, 0, 1, 1, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                },
                new double[]
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //circle
                    0, 0, 1, 1, 1, 1, 0, 0, 0, 0,
                    0, 1, 1, 0, 1, 1, 1, 0, 0, 0,
                    0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
                    0, 1, 1, 0, 0, 0, 1, 1, 1, 0,
                    0, 1, 1, 0, 0, 0, 1, 1, 0, 0,
                    0, 0, 1, 0, 0, 0, 1, 1, 0, 0,
                    0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
                    0, 0, 0, 1, 1, 1, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                },
                new double[]
                {
                    1, 0, 0, 0, 0, 0, 0, 0, 0, 1, //cross
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 1,
                    0, 0, 1, 0, 0, 0, 0, 0, 1, 0,
                    0, 0, 0, 1, 0, 0, 0, 1, 0, 0,
                    0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
                    0, 0, 0, 1, 1, 1, 1, 0, 0, 0,
                    0, 0, 1, 0, 0, 0, 0, 1, 0, 0,
                    0, 1, 0, 0, 0, 0, 0, 0, 1, 0,
                    1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                    1, 0, 0, 0, 0, 0, 0, 0, 0, 0
                },
            };
        }
    }
}