using System.Collections.Generic;

namespace Extensions
{
    public static class ListExtension
    {
        public static T GetRandomItem<T>(this IList<T> list)
        {
            T result = list[UnityEngine.Random.Range(0, list.Count)];
            return result;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list.Swap(i, UnityEngine.Random.Range(i, list.Count));
            }
        }

        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}

