namespace Monads
{
    public class Maybe<T>
    {
        public readonly static Maybe<T> Nothing = new Maybe<T>();

        public Maybe(T value)
        {
            Value = value;
            HasValue = true;
        }

        Maybe()
        {
            HasValue = false;
        }

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

        public override string ToString()
        {
            return HasValue ? Value.ToString() : "Nothing";
        }
    }
}
