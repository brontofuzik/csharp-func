using System.Collections.Generic;
using System;

namespace Monads
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ToList<T>(this T value)
        {
            yield return value;
        }

        public static IEnumerable<U> SelectMany<T, U>(this IEnumerable<T> m, Func<T, IEnumerable<U>> k)
        {
            foreach (var x in m)
                foreach (var y in k(x))
                    yield return y;
        }
    }
}
