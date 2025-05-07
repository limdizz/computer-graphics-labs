using System.Drawing;

namespace Filters
{
    class BrightnessUpFilter : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(Clamp(sourceColor.R + 10, 0, 255), Clamp(sourceColor.G + 10, 0, 255), Clamp(sourceColor.B + 10, 0, 255));
            return resultColor;
        }
    }
}
