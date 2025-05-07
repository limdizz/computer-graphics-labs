using System.Drawing;

namespace Filters
{
    class SepiaFilter : Filters
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int k = 20;
            Color sourceColor = sourceImage.GetPixel(x, y);
            double intensity = 0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B;
            int r = Clamp((int)(intensity + 2 * k), 0, 255);
            int g = Clamp((int)(intensity + 0.5 * k), 0, 255);
            int b = Clamp((int)(intensity - 1 * k), 0, 255);
            return Color.FromArgb(r, g, b);
        }
    }
}
