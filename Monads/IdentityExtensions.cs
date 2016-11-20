using System;

namespace Monads
{
    public static class IdentityExtensions
    {
        // Unit/ToIdentity

        public static Identity<T> Unit<T>(this T value)
        {
            return new Identity<T>(value);
        }

        public static Identity<T> ToIdentity<T>(this T value)
        {
            return new Identity<T>(value);
        }

        // Bind/SelectMany

        public static Identity<U> Bind<T, U>(this Identity<T> id, Func<T, Identity<U>> k)
        {
            return k(id.Value);
        }

        public static Identity<U> SelectMany<T, U>(this Identity<T> id, Func<T, Identity<U>> k)
        {
            return k(id.Value);
        }

        public static Identity<V> SelectMany<T, U, V>(this Identity<T> id, Func<T, Identity<U>> k, Func<T, U, V> s)
        {
            return id.SelectMany(x => k(x).SelectMany(y => s(x, y).ToIdentity()));
        }
    }
}
