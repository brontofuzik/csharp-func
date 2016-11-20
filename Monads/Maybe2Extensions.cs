using System;

namespace Monads
{
    public static class Maybe2Extensions
    {
        public static Maybe2<T> ToMaybe2<T>(this T value)
        {
            return Maybe2<T>.Just(value);
        }

        public static Maybe2<B> Bind<A, B>(this Maybe2<A> a, Func<A, Maybe2<B>> func)
        {
            return a.HasValue ? func(a.Value) : Maybe2<B>.Nothing();
        }

        public static Maybe2<C> SelectMany<A, B, C>(this Maybe2<A> a, Func<A, Maybe2<B>> func, Func<A, B, C> select)
        {
            return a.Bind(aval => func(aval).Bind(bval => select(aval, bval).ToMaybe2()));
        }
    }
}
