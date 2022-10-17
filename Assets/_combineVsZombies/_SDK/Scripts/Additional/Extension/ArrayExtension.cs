namespace Extensions
{
    public static class ArrayExtension
    {
        public static bool Contains<T>(this T[] array, T item)
        {
            bool result = false;

            if (array != null && item != null)
            {
                foreach (var x in array)
                {
                    if (x != null && x.Equals(item))
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }
    }
}

