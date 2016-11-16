using System;
using System.Drawing;
using System.Linq;

namespace TicTacToe.Model
{
    class BitmapConverter
    {
        public static double[] ImageToByte(Bitmap bitmap)
        {
            double[,] byteImage = new double[bitmap.Height,bitmap.Width];
            int i = 0;
            for (int column = 0; column < bitmap.Height; column++)
            {
                for (int row = 0; row < bitmap.Width; row++)
                {
                    Color color = bitmap.GetPixel(column, row);
                    if (color.A == 255)
                    {

                        byteImage[column,row] = 1;
                        i++;
                    }
                    else
                    {
                        byteImage[column, row] = 0;
                        i++;
                    }
                }
            }
            double[] byteImageMatrix = CreateByteImageMatrix(byteImage);
            return byteImageMatrix;
        }

        public static double[] CreateByteImageMatrix(double[,] byteImage)
        {
            int count1 = 0;
            int count2 = 0;
            double[,] byteImageMatrix = new double[10,10];
            for (int i = 0; i < byteImageMatrix.Length; i++)
            {
                for (int j = 0; j < 99; j++)
                {
                    if (byteImage[i,j] == 1 && count1 < 99 && count2<99)
                    {
                        count1 = i / 10;
                        count2 = j/10;
                        byteImageMatrix[count1,count2] = 1;
                        j = j + Math.Abs(10 - j);
                    }
                }
            }
            double[] matrix = ConvertToOneSize(byteImageMatrix);
            return matrix;
        }

        private static double[] ConvertToOneSize(double[,] byteImageMatrix)
        {
            double[] matrix = byteImageMatrix.Cast<double>().ToArray(); //new double[100];
            //Buffer.BlockCopy(byteImageMatrix,0,matrix,0,byteImageMatrix.Length);
            return matrix;
        }
    }
}
