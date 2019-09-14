using System;
using System.Collections.Generic;
using System.Linq;

namespace senrenbanka.murasame.qqbot.Resources.Generic
{
    public static class Enumerates
    {
        public static void Update<T>(this IEnumerable<T> enumerable, Func<T, bool> keySelector, Action<T> updater)
        {
            var element = enumerable.FirstOrDefault(keySelector);
            if (element != null)
            {
                updater(element);
            }
        }

        public static void ForEachIndexed<T>(this IEnumerable<T> enumerable, Action<int, T> action)
        {
            var ts = enumerable as T[] ?? enumerable.ToArray();
            for (var i = 0; i < ts.Length; i++)
            {
                action(i, ts[i]);
            }
        }

        public static bool EqualsIndexed<T, I>(this IEnumerable<T> enumerable, IEnumerable<T> other, Func<T, I> transformer)
        {
            var arr = other.ToArray();
            return !enumerable.Where((t, i) => !transformer(t).Equals(transformer(arr[i]))).Any();
        }

        public static bool EqualsIndexed<T>(this IEnumerable<T> enumerable, IEnumerable<T> other)
        {
            var arr = other.ToArray();
            return !enumerable.Where((t, i) => t.Equals(arr[i])).Any();
        }

        public static void RemoveWhere<T>(this ISet<T> enumerable, Func<T, bool> key)
        {
            var value = enumerable.FirstOrDefault(key);

            if (value != null)
            {
                enumerable.Remove(value);
            }
        }
    }
}