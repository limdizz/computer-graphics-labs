using System.Collections.Generic;
using System.Drawing;

namespace Filters
{
    class GreyWorldFilter : Filters
    {
        private int R_, G_, B_, Average;
        private bool flag = false;

        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            if (!flag)
            {
                List<int> RGB = AverageBrightness(sourceImage);
                R_ = RGB[0];
                G_ = RGB[1];
                B_ = RGB[2];
                Average = (R_ + G_ + B_) / 3;
                flag = true;
                if (R_ == 0)
                {
                    R_++;
                }
                if (G_ == 0)
                {
                    G_++;
                }
                if (B_ == 0)
                {
                    B_++;
                }
            }
            Color resultColor = Color.FromArgb(Clamp(sourceColor.R * Average / R_, 0, 255), Clamp(sourceColor.G * Average / G_, 0, 255), Clamp(sourceColor.B * Average / B_, 0, 255));
            return resultColor;
        }

        public List<int> AverageBrightness(Bitmap sourceImage)
        {
            List<int> RGB = new List<int>();
            R_ = 0;
            G_ = 0;
            B_ = 0;
            int N = sourceImage.Width * sourceImage.Height;
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    R_ += sourceImage.GetPixel(i, j).R;
                    G_ += sourceImage.GetPixel(i, j).G;
                    B_ += sourceImage.GetPixel(i, j).B;
                }
            }

            R_ /= N;
            G_ /= N;
            B_ /= N;

            RGB.Add(R_);
            RGB.Add(G_);
            RGB.Add(B_);
            return RGB;
        }
    }
}
