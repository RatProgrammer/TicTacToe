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
            //double[] byteImageMatrix = CreateByteImageMatrix(byteImage);
            return byteImage; //byteImageMatrix;
        }

        public static double[] CreateByteImageMatrix(double[] byteImage)
        {
            int count = 0;
            //int parameter;
            double[] byteImageMatrix = new double[625];
            //foreach (var v in byteImage)
            //{
            //    for (int i = 0; i < 1000; i = i + 10)
            //    {
            //        for (int j = 0; j < 10; j ++)
            //        {
            //            if (byteImage[j + i] == 1 && count < 100)
            //            {
            //                parameter = (j+i)/10;
            //                byteImageMatrix[parameter] = 1;
            //                j = 100;
            //            }
            //        }
            //        count++;
            //    }
            //}
            for (int i = 0; i < byteImage.Length-4; i=i+5)
            {
                for (int j = 0; j < 5; j++)
                {

                    if (byteImage[j + i] == 1 && count<625)
                    {
                        byteImageMatrix[count] = 1;
                        j = 5;
                    }

                }
                count++;

            }

            return byteImageMatrix;
        }
    }
}
