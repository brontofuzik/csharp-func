using System;

namespace Monads
{
    class Program
    {
        static void Main(string[] args)
        {
            IdentityComposition1();
            IdentityComposition2();
            IdentityComposition3();
            IdentityComposition4();
            IdentityComposition5();

            MaybeComposition();

            SimpleComposition();
            Maybe2Composition();
            AtrbitraryComposition();

            ListComposition();
        }

        #region Identity monad

        // Unit/Bind
        public static void IdentityComposition1()
        {
            var result =
                Identity<int>.Bind(Identity<int>.Unit(5), x =>
                Identity<int>.Bind(Identity<int>.Unit(6), y =>
                Identity<int>.Unit(x + y)));

            Console.WriteLine(result.Value);
        }

        // Unit/Bind - Extension methods
        public static void IdentityComposition2()
        {
            var result =
                5.Unit().Bind(x =>
                6.Unit().Bind(y =>
                (x + y).Unit()));

            Console.WriteLine(result.Value);
        }

        // Nested SelectMany
        public static void IdentityComposition3()
        {
            var result =
                5.ToIdentity().SelectMany(x =>
                6.ToIdentity().SelectMany(y =>
                (x + y).ToIdentity()));

            Console.WriteLine(result.Value);
        }

        // Non-nested SelectMany
        public static void IdentityComposition4()
        {
            var result =
                5.ToIdentity().SelectMany(x =>
                6.ToIdentity(), (x, y) => x + y);

            Console.WriteLine(result.Value);
        }

        // Query comprehensions
        public static void IdentityComposition5()
        {
            var result =
                from x in 5.ToIdentity()
                from y in 6.ToIdentity()
                select x + y;

            Console.WriteLine(result.Value);
        }

        #endregion

        #region Maybe monad

        // Query comprehensions
        private static void MaybeComposition()
        {
            var result =
                from x in 5.ToMaybe()
                from y in Maybe<int>.Nothing
                select x + y;

            Console.WriteLine(result.ToString());
        }

        #endregion

        #region Maybe2 monad

        public static void SimpleComposition()
        {
            Func<int, int> add2 = x => x + 2;
            Func<int, int> multiplyBy2 = x => x * 2;
            Func<int, int> add2MultiplyBy2 = x => multiplyBy2(add2(x));
            Console.WriteLine("add2MultiplyBy2(3) = {0}", add2MultiplyBy2(3));
        }

        public static void Maybe2Composition()
        {
            Func<int, Maybe2<int>> add2 = x => (x != 0) ? Maybe2<int>.Just(x + 2) : Maybe2<int>.Nothing();
            Func<int, Maybe2<int>> multiplyBy2 = x => (x != 0) ? Maybe2<int>.Just(x * 2) : Maybe2<int>.Nothing();
            Func<int, Maybe2<int>> add2MultiplyBy2 = x => add2(x).Bind(multiplyBy2);
            var result = add2MultiplyBy2(3);
            Console.WriteLine("result = {0}", result);
        }

        public static void AtrbitraryComposition()
        {
            var maybeInt = 5.ToMaybe2();
            var maybeString = "Hello!".ToMaybe2();
            var maybeDateTime = DateTime.Now.ToMaybe2();
            var result =
                maybeInt.Bind(aval =>
                maybeString.Bind(bval =>
                maybeDateTime.Bind(cval =>
                    String.Format("{0} {1} {2}", aval, bval, cval).ToMaybe2()
                )));
            Console.WriteLine("result = {0}", result);

            var result2 =
                from a in maybeInt
                from b in maybeString
                from c in maybeDateTime
                select String.Format("{0} {1} {2}", a, b, c);
        }

        #endregion

        #region List monad

        // Query comprehensions
        public static void ListComposition()
        {
            var result =
                from x in new[] { 0, 1, 2 }
                from y in new[] { 0, 1, 2 }
                select x + y;

            foreach (var i in result)
                Console.WriteLine(i);
        }

        #endregion

        #region Continuation monad


        #endregion
    }
}
