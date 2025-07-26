namespace OneBeyondApi.Helpers
{
    public static class TextHelper
    {
        public static string ProvideIsAre(int count)
        {
            if (count < 1)
            {
                throw new ArgumentException("Count must be at least 1.", nameof(count));
            }

            return count == 1 ? "is" : "are";
        }

        public static string ProvidePlural(int count)
        {
            if (count < 1)
            {
                throw new ArgumentException("Count must be at least 1.", nameof(count));
            }

            return count == 1 ? string.Empty : "s";
        }
    }
}
