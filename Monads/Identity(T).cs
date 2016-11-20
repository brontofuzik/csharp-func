using System;

namespace Monads
{
    public class Identity<T>
    {
        public Identity(T value)
        {
            Value = value;
        }

        public T Value
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        // Unit

        public static Identity<T> Unit(T value)
        {
            return new Identity<T>(value);
        }

        // Bind

        public static Identity<U> Bind<U>(Identity<T> id, Func<T, Identity<U>> k)
        {
            return k(id.Value);
        }
    }
}
