using System;

namespace Monads
{
    public static class MaybeExtensions
    {
        public static Maybe<T> ToMaybe<T>(this T value)
        {
            return new Maybe<T>(value);
        }

        public static Maybe<U> SelectMany<T, U>(this Maybe<T> m, Func<T, Maybe<U>> k)
        {
            if (!m.HasValue)
            {
                return Maybe<U>.Nothing;
            }
            return k(m.Value);
        }
    }
}
