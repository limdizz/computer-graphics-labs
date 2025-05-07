using System.Drawing;

namespace Filters
{
    class TransferFilter : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int xR, yR;
            xR = x + 50;
            yR = y;

            int xRes = Clamp(xR, 0, sourceImage.Width - 1);
            int yRes = Clamp(yR, 0, sourceImage.Height - 1);

            Color sourceColor = sourceImage.GetPixel(xRes, yRes);

            return sourceColor;
        }
    }
}
