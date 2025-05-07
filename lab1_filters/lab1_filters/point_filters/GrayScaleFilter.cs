using System.Drawing;

namespace Filters
{
    class GrayScaleFilter : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            double intensity = 0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B;
            Color resultColor = Color.FromArgb((int)intensity, (int)intensity, (int)intensity);
            return resultColor;
        }
    }
}
