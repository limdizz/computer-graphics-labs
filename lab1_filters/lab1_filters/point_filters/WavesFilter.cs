using System;
using System.Drawing;

namespace Filters
{
    class WavesFilterX : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int xR, yR;
            xR = (int)(x + 20 * Math.Sin((2 * Math.PI * y) / 60));
            yR = y;

            int xRes = Clamp(xR, 0, sourceImage.Width - 1);
            int yRes = Clamp(yR, 0, sourceImage.Height - 1);

            Color sourceColor = sourceImage.GetPixel(xRes, yRes);

            return sourceColor;
        }
    }

    class WavesFilterY : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int xR, yR;
            xR = x;
            yR = (int)(y + 20 * Math.Sin((2 * Math.PI * x) / 30));

            int xRes = Clamp(xR, 0, sourceImage.Width - 1);
            int yRes = Clamp(yR, 0, sourceImage.Height - 1);

            Color sourceColor = sourceImage.GetPixel(xRes, yRes);

            return sourceColor;
        }

    }
}
