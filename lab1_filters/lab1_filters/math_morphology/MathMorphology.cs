using System.ComponentModel;
using System.Drawing;

namespace Filters
{
    class MathMorphology : Filters
    {
        protected bool isDilation;
        protected int[,] kernel = null;
        protected MathMorphology()
        {
            kernel = new int[,] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } };
        }
        public MathMorphology(int[,] mask)
        {
            this.kernel = mask;
        }
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            int minR = 255; int minG = 255; int minB = 255;
            int maxR = 0; int maxG = 0; int maxB = 0;
            for (int i = -radiusX; i <= radiusX; i++)
                for (int j = -radiusY; j <= radiusY; j++)
                {
                    if (isDilation)
                    {
                        if ((kernel[i + radiusX, j + radiusY] != 0) && (sourceImage.GetPixel(x + i, y + j).R > maxR))
                            maxR = sourceImage.GetPixel(x + i, y + j).R;
                        if ((kernel[i + radiusX, j + radiusY] != 0) && (sourceImage.GetPixel(x + i, y + j).G > maxG))
                            maxG = sourceImage.GetPixel(x + i, y + j).G;
                        if ((kernel[i + radiusX, j + radiusY] != 0) && (sourceImage.GetPixel(x + i, y + j).B > maxB))
                            maxB = sourceImage.GetPixel(x + i, y + j).B;
                    }
                    else
                    {
                        if ((kernel[i + radiusX, j + radiusY] != 0) && (sourceImage.GetPixel(x + i, y + j).R < minR))
                            minR = sourceImage.GetPixel(x + i, y + j).R;
                        if ((kernel[i + radiusX, j + radiusY] != 0) && (sourceImage.GetPixel(x + i, y + j).G < minG))
                            minG = sourceImage.GetPixel(x + i, y + j).G;
                        if ((kernel[i + radiusX, j + radiusY] != 0) && (sourceImage.GetPixel(x + i, y + j).B < minB))
                            minB = sourceImage.GetPixel(x + i, y + j).B;
                    }
                }
            if (isDilation)
                return Color.FromArgb(maxR, maxG, maxB);
            else
                return Color.FromArgb(minR, minG, minB);
        }
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = radiusX; i < sourceImage.Width - radiusX; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = radiusY; j < sourceImage.Height - radiusY; j++)
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
            }
            return resultImage;
        }

    }
    class Dilation : MathMorphology // Морфологическое расширение
    {
        public Dilation()
        {
            isDilation = true;
        }
        public Dilation(int[,] mask)
        {
            this.kernel = mask;
            isDilation = true;
        }
    }

    class Erosion : MathMorphology // Морфологическое сужение
    {
        public Erosion()
        {
            isDilation = false;
        }
        public Erosion(int[,] mask)
        {
            this.kernel = mask;
            isDilation = false;
        }
    }

    class Opening : MathMorphology // Морфологическое открытие
    {

        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return sourceImage.GetPixel(x, y);
        }
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            Filters filter1 = new Erosion(kernel);
            Bitmap result = filter1.processImage(sourceImage, worker);
            Filters filter2 = new Dilation(kernel);
            result = filter2.processImage(result, worker);
            return result;
        }
    }

    class Closing : MathMorphology // Морфологическое закрытие
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return sourceImage.GetPixel(x, y);
        }
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            Filters filter1 = new Dilation(kernel);
            Bitmap result = filter1.processImage(sourceImage, worker);
            Filters filter2 = new Erosion(kernel);
            result = filter2.processImage(result, worker);
            return result;
        }
    }

    class TopHat : MathMorphology // TopHat (цилиндр)
    {
        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return sourceImage.GetPixel(x, y);
        }
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap result = new Bitmap(sourceImage.Width, sourceImage.Height);
            Filters filter1 = new Erosion(kernel);
            Bitmap result1 = filter1.processImage(sourceImage, worker);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / sourceImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    int newR = Clamp(sourceImage.GetPixel(i, j).R - result1.GetPixel(i, j).R, 0, 255);
                    int newG = Clamp(sourceImage.GetPixel(i, j).G - result1.GetPixel(i, j).G, 0, 255);
                    int newB = Clamp(sourceImage.GetPixel(i, j).B - result1.GetPixel(i, j).B, 0, 255);
                    result.SetPixel(i, j, Color.FromArgb(newR, newG, newB));
                }
            }
            return result;
        }
    }
}
