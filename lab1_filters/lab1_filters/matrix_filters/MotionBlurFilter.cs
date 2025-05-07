namespace Filters
{
    class MotionBlurFilter : MatrixFilter
    {
        public MotionBlurFilter()
        {
            int n = 9;
            kernel = new float[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        kernel[i, j] = 1.0f / (float)(n);
                    }
                    else
                    {
                        kernel[i, j] = 0;
                    }
                }
            }
        }

    }
}
