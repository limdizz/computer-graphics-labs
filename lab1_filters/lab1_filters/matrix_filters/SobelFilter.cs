using System;
using System.Drawing;

namespace Filters
{
    class SobelXFilter : MatrixFilter
    {
        public SobelXFilter() : base(new float[,] {
            {-1, 0, 1 },
            {-2, 0, 2 },
            {-1, 0, 1 }
        })
        {
        }
    }

    class SobelYFilter : MatrixFilter
    {
        public SobelYFilter() : base(new float[,] {
            {-1, 2, -1 },
            {0, 0, 0 },
            {1, 2, 1 }
        })
        {
        }
    }

    class SobelFilter : Filters
    {
        private SobelXFilter sobelX;
        private SobelYFilter sobelY;
        public SobelFilter()
        {
            sobelX = new SobelXFilter();
            sobelY = new SobelYFilter();

        }

        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            float gradientX = sobelX.calculateNewPixelColor(sourceImage, x, y).R;
            float gradientY = sobelY.calculateNewPixelColor(sourceImage, x, y).R;

            float gradientValue = (float)Math.Sqrt(gradientX * gradientX + gradientY * gradientY);

            return Color.FromArgb(Clamp((int)gradientValue, 0, 255), Clamp((int)gradientValue, 0, 255), Clamp((int)gradientValue, 0, 255));
        }
    }
}
