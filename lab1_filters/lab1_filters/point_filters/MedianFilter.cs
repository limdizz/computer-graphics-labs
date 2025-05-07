using System.Collections.Generic;
using System.Drawing;

namespace Filters
{
    class MedianFilter : Filters
    {
        public override Color calculateNewPixelColor(Bitmap image, int x, int y)
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
    }
}
