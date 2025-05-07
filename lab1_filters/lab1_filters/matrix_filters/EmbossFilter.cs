using System.Drawing;

namespace Filters
{
    class EmbossFilterMatrix : MatrixFilter
    {
        public EmbossFilterMatrix() : base(new float[,]
        {
            {0, 1, 0},
            {1, 0, -1},
            {0, -1, 0 }
        })
        { }
    }

    class EmbossFilter : Filters
    {
        private EmbossFilterMatrix emboss;
        public EmbossFilter()
        {
            emboss = new EmbossFilterMatrix();
        }

        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = emboss.calculateNewPixelColor(sourceImage, x, y);
            Color resultColor = Color.FromArgb(Clamp(sourceColor.R + 10, 0, 255), Clamp(sourceColor.G + 10, 0, 255), Clamp(sourceColor.B + 10, 0, 255));
            return resultColor;
        }
    }
}
