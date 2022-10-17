namespace Extensions
{
    public static class IntExtension
    {
        public static bool IsBetween(this int number, int firstNumber, int secondNumber)
        {
            return firstNumber <= number && number <= secondNumber;
        }
    }
}
