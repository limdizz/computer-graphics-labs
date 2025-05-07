using System;
using System.Collections.Generic;
using System.Drawing;

namespace Filters
{
    class GlowingEdgesFilter : Filters
    {
        private int[,] sobelX = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
        private int[,] sobelY = new int[,] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color medianColor = MedianFilter(sourceImage, x, y);
            Color edgeColor = EdgeDetection(sourceImage, x, y);
            return MaximumFilter(sourceImage, x, y, medianColor, edgeColor);
        }

        private Color MedianFilter(Bitmap image, int x, int y)
        {
            List<int> reds = new List<int>();
            List<int> greens = new List<int>();
            List<int> blues = new List<int>();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newX = Clamp(x + i, 0, image.Width - 1);
                    int newY = Clamp(y + j, 0, image.Height - 1);
                    Color neighborColor = image.GetPixel(newX, newY);
                    reds.Add(neighborColor.R);
                    greens.Add(neighborColor.G);
                    blues.Add(neighborColor.B);
                }
            }

            reds.Sort(); greens.Sort(); blues.Sort();
            return Color.FromArgb(reds[4], greens[4], blues[4]);
        }

        private Color EdgeDetection(Bitmap image, int x, int y)
        {
            int gxR = 0, gxG = 0, gxB = 0;
            int gyR = 0, gyG = 0, gyB = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newX = Clamp(x + i, 0, image.Width - 1);
                    int newY = Clamp(y + j, 0, image.Height - 1);
                    Color neighborColor = image.GetPixel(newX, newY);

                    gxR += sobelX[i + 1, j + 1] * neighborColor.R;
                    gxG += sobelX[i + 1, j + 1] * neighborColor.G;
                    gxB += sobelX[i + 1, j + 1] * neighborColor.B;

                    gyR += sobelY[i + 1, j + 1] * neighborColor.R;
                    gyG += sobelY[i + 1, j + 1] * neighborColor.G;
                    gyB += sobelY[i + 1, j + 1] * neighborColor.B;
                }
            }

            int edgeR = Clamp((int)Math.Sqrt(gxR * gxR + gyR * gyR), 0, 255);
            int edgeG = Clamp((int)Math.Sqrt(gxG * gxG + gyG * gyG), 0, 255);
            int edgeB = Clamp((int)Math.Sqrt(gxB * gxB + gyB * gyB), 0, 255);

            return Color.FromArgb(edgeR, edgeG, edgeB);
        }

        private Color MaximumFilter(Bitmap image, int x, int y, Color medianColor, Color edgeColor)
        {
            int maxR = Math.Max(medianColor.R, edgeColor.R);
            int maxG = Math.Max(medianColor.G, edgeColor.G);
            int maxB = Math.Max(medianColor.B, edgeColor.B);

            return Color.FromArgb(maxR, maxG, maxB);
        }
    }
}
