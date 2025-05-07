using System;
using System.Drawing;

namespace Filters
{
    class ScharrOperatorXFilter : MatrixFilter
    {
        public ScharrOperatorXFilter() : base(new float[,] {
            {3, 0, -3 },
            {10, 0, -10 },
            {3, 0, -3 }
        })
        {
        }
    }

    class ScharrOperatorYFilter : MatrixFilter
    {
        public ScharrOperatorYFilter() : base(new float[,] {
            {3, 10, 3 },
            {0, 0, 0 },
            {-3, -10, -3 }
        })
        {
        }
    }

    class ScharrOperatorFilter : Filters
    {
        private ScharrOperatorXFilter scharrX;
        private ScharrOperatorYFilter scharrY;

        public ScharrOperatorFilter()
        {
            scharrX = new ScharrOperatorXFilter();
            scharrY = new ScharrOperatorYFilter();

        }

        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            float gradientX = scharrX.calculateNewPixelColor(sourceImage, x, y).R;
            float gradientY = scharrY.calculateNewPixelColor(sourceImage, x, y).R;

            float gradientValue = (float)Math.Sqrt(gradientX * gradientX + gradientY * gradientY);

            return Color.FromArgb(Clamp((int)gradientValue, 0, 255), Clamp((int)gradientValue, 0, 255), Clamp((int)gradientValue, 0, 255));
        }
    }
}
