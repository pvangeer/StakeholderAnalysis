namespace StakeholderAnalysis.Storage
{
    public static class DoubleConversionExtensions
    {
        public static double ToNullAsNaN(this double? value)
        {
            if (value == null) return double.NaN;
            return value.Value;
        }
    }
}