using System.Drawing;

namespace Filters
{
    class BWInvertFilter : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);

            int middleThirdStart = sourceImage.Height / 3;
            int middleThirdEnd = 2 * sourceImage.Height / 3;

            if (y >= middleThirdStart && y < middleThirdEnd)
            {
                double intensity = 0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B;
                return Color.FromArgb((int)intensity, (int)intensity, (int)intensity);
            }
            else
            {
                return Color.FromArgb(255 - sourceColor.R, 255 - sourceColor.G, 255 - sourceColor.B);
            }
        }
    }
}
