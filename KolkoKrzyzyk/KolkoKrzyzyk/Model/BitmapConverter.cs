using System;
using System.Drawing;

namespace TicTacToe.Model
{
    class BitmapConverter
    {
        public static double[] ImageToByte(Bitmap bitmap)
        {
            double[] byteImage = new double[bitmap.Height*bitmap.Width];
            int i = 0;
            for (int column = 0; column < bitmap.Height; column++)
            {
                for (int row = 0; row < bitmap.Width; row++)
                {
                    Color color = bitmap.GetPixel(column, row);
                    if (color.A == 255)
                    {

                        byteImage[i] = 1;
                        i++;
                    }
                    else
                    {
                        byteImage[i]= 0;
                        i++;
                    }
                }
            }
            double[] byteImageMatrix = CreateByteImageMatrix(byteImage);
            return byteImageMatrix;
        }

        public static double[] CreateByteImageMatrix(double[] byteImage)
        {
            int count = 0;
            double[] byteImageMatrix = new double[100];

            for (int i = 0; i < byteImage.Length-4; i=i+100)
            {
                for (int j = 0; j < 100; j++)
                {

                    if (byteImage[j + i] == 1 && count<99)
                    {
                        byteImageMatrix[count] = 1;
                        j = 100;
                    }

                }
                count++;

            }

            return byteImageMatrix;
        }
    }
}
