namespace Extensions
{
    public static class StringExtension
    {
        public static string Formatted(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }

    public static class NumberFormatter
    {
        public static string GetFormatedNumber(int amount)
        {
            if (amount >= 100000000)
            {
                return (amount / 1000000D).ToString("0.#M");
            }
            if (amount >= 1000000)
            {
                return (amount / 1000000D).ToString("0.##M");
            }
            if (amount >= 100000)
            {
                return (amount / 1000D).ToString("0.#k");
            }
            if (amount >= 10000)
            {
                return (amount / 1000D).ToString("0.##k");
            }

            return amount.ToString();
        }
    }
}
