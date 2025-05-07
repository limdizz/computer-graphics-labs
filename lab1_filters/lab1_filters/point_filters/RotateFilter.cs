using System;
using System.Drawing;

namespace Filters
{
    class RotateFilter : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int xR, yR;
            int x0 = sourceImage.Width / 2;
            int y0 = sourceImage.Height / 2;

            double alpha = Math.PI / 4;

            xR = Clamp((int)((x - x0) * Math.Cos(alpha) - (y - y0) * Math.Sin(alpha) + x0), 0, sourceImage.Width - 1);
            yR = Clamp((int)((x - x0) * Math.Sin(alpha) + (y - y0) * Math.Cos(alpha) + y0), 0, sourceImage.Height - 1);

            Color sourceColor = sourceImage.GetPixel(xR, yR);

            return sourceColor;
        }
    }
}
