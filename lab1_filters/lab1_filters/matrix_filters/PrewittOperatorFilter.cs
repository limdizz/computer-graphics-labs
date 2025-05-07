using System;
using System.Drawing;

namespace Filters
{
    class PrewittOperatorXFilter : MatrixFilter
    {
        public PrewittOperatorXFilter() : base(new float[,] {
            {-1, 0, 1 },
            {-1, 0, 1 },
            {-1, 0, 1}
        })
        {
        }
    }

    class PrewittOperatorYFilter : MatrixFilter
    {
        public PrewittOperatorYFilter() : base(new float[,] {
            {-1, -1, -1 },
            {0, 0, 0 },
            {1, 1, 1}
        })
        {
        }
    }

    class PrewittOperatorFilter : Filters
    {
        private PrewittOperatorXFilter prewittX;
        private PrewittOperatorYFilter prewittY;

        public PrewittOperatorFilter()
        {
            prewittX = new PrewittOperatorXFilter();
            prewittY = new PrewittOperatorYFilter();
        }
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            float gradientX = prewittX.calculateNewPixelColor(sourceImage, x, y).R;
            float gradientY = prewittY.calculateNewPixelColor(sourceImage, x, y).R;

            float gradientValue = (float)Math.Sqrt(gradientX * gradientX + gradientY * gradientY);

            return Color.FromArgb(Clamp((int)gradientValue, 0, 255), Clamp((int)gradientValue, 0, 255), Clamp((int)gradientValue, 0, 255));
        }
    }
}
