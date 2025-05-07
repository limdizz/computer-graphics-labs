using System;
using System.Drawing;

namespace Filters
{
    class GlassEffectFilter : Filters
    {
        Random rand;
        public GlassEffectFilter()
        {
            rand = new Random();
        }

        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int r = rand.Next(-20, 20);
            int r2 = rand.Next(-20, 20);

            int xR = (int)(x + r);
            int yR = (int)(y + r2);

            int xRes = Clamp(xR, 0, sourceImage.Width - 1);
            int yRes = Clamp(yR, 0, sourceImage.Height - 1);

            Color sourceColor = sourceImage.GetPixel(xRes, yRes);

            return sourceColor;
        }
    }
}
