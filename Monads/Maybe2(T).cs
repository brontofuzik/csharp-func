namespace Monads
{
    public class Maybe2<T>
    {
        public T Value
        {
            get;
            private set;
        }

        public bool HasValue
        {
            get;
            private set;
        }

        public static Maybe2<T> Just(T value)
        {
            return new Maybe2<T>
            {
                Value = value,
                HasValue = true
            };
        }

        public static Maybe2<T> Nothing()
        {
            return new Maybe2<T>
            {
                Value = default(T),
                HasValue = false
            };
        }

        public override string ToString()
        {
            return HasValue ? Value.ToString() : "Nothing";
        }
    }
}
