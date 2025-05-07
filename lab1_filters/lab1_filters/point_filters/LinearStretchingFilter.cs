using System.Drawing;

namespace Filters
{
    class LinearStretchingFilter : Filters
    {
        private int minR, minG, minB, maxR, maxG, maxB;
        private bool flag = false;
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            if (!flag)
            {
                calculateDelta(sourceImage);
                flag = true;
            }
            int r = Clamp((sourceColor.R - minR) * 255 / (maxR - minR), 0, 255);
            int g = Clamp((sourceColor.G - minG) * 255 / (maxG - minG), 0, 255);
            int b = Clamp((sourceColor.B - minB) * 255 / (maxB - minB), 0, 255);

            return Color.FromArgb(r, g, b);
        }

        public void calculateDelta(Bitmap sourceImage)
        {
            minR = minG = minB = 255;
            maxR = maxG = maxB = 0;

            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color color = sourceImage.GetPixel(i, j);

                    if (color.R < minR) minR = color.R;
                    if (color.G < minG) minG = color.G;
                    if (color.B < minB) minB = color.B;

                    if (color.R > maxR) maxR = color.R;
                    if (color.G > maxG) maxG = color.G;
                    if (color.B > maxB) maxB = color.B;
                }
            }
        }
    }


}
