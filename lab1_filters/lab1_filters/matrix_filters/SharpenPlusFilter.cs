namespace Filters
{
    class SharpenPlusFilter : MatrixFilter
    {
        public SharpenPlusFilter() : base(new float[,]
        {
            {-1, -1, -1},
            {-1, 9, -1},
            {-1, -1, -1}
        })
        { }
    }
}
