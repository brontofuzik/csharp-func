using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCombinator
{
    class Program
    {
        static void Main(string[] args)
        {
            WesDyer.Test();
            Enigmativity.Test();
        }
    }

    /// <summary>
    /// Wes Dyer (MSDN)
    /// https://blogs.msdn.microsoft.com/wesdyer/2007/02/02/anonymous-recursion-in-c/
    /// </summary>
    class WesDyer
    {
        public static void Test()
        {
            Test7();
        }  

        private static void Test0()
        {
            Func<int, int> fib = null;
            fib = n => n > 1 ? fib(n-1) + fib(n-2) : n;
            Console.WriteLine($"Wes Dyer: fib(6) = {fib(6)}"); // 8
        }

        // Recursive1<A, R> :: (Recursive1<A, R>, A) -> R
        delegate R Recursive1<A, R>(Recursive1<A, R> rec, A arg);

        // Test with Recursive1
        private static void Test1()
        {
            Recursive1<int, int> fib = (/*Recursive1<int, int>*/ f, /*int*/ n) => n > 1 ? f(f, n-1) + f(f, n-2) : n;
            Console.WriteLine($"Wes Dyer: fib(fib, 6) = {fib(fib, 6)}"); // 8
        }

        // Recursive2<A, R> :: Recursive2<A, R> -> (A -> R)
        delegate Func<A, R> Recursive2<A, R>(Recursive2<A, R> rec);

        // Test with Recursive2
        private static void Test2()
        {
            Recursive2<int, int> fibRec = f => n => n > 1 ? f(f)(n-1) + f(f)(n-2) : n;
            Func<int, int> fib = fibRec(fibRec);
            Console.WriteLine($"Wes Dyer: fib(6) = {fib(6)}"); // 8
        }

        private static void Test3()
        {
            Recursive2<int, int> fibRec = f => n =>
            {
                Func<Func<int, int>, int, int> g = (h, m) => m > 1 ? h(m-1) + h(m-2) : m;
                return g(f(f), n);
            };
            Func<int, int> fib = fibRec(fibRec);
            Console.WriteLine($"Wes Dyer: fib(6) = {fib(6)}"); // 8
        }

        // Currying the inner lambda
        private static void Test4()
        {
            Recursive2<int, int> fibRec = f => n =>
            {
                Func<Func<int, int>, Func<int, int>> g = h => m => m > 1 ? h(m-1) + h(m-2) : m;
                return g(f(f))(n);
            };
            Func<int, int> fib = fibRec(fibRec);
            Console.WriteLine($"Wes Dyer: fib(6) = {fib(6)}"); // 8
        }

        // Moving g outside
        private static void Test5()
        {
            Func<Func<int, int>, Func<int, int>> g = h => m => m > 1 ? h(m-1) + h(m-2) : m;
            Recursive2<int, int> fibRec = f => n => g(f(f))(n);
            Func<int, int> fib = fibRec(fibRec);
            Console.WriteLine($"Wes Dyer: fib(6) = {fib(6)}"); // 8
        }

        // CreateFib
        static Func<int, int> CreateFib(Func<Func<int, int>, Func<int, int>> g)
        {
            Recursive2<int, int> fibRec = f => n => g(f(f))(n);
            return fibRec(fibRec);
        }

        // Using CreateFib()
        private static void Test6()
        {
            Func<Func<int, int>, Func<int, int>> g = h => m => m > 1 ? h(m-1) + h(m-2) : m;
            Func<int, int> fib = CreateFib(g);
            Console.WriteLine(fib(6));
        }

        // Y
        private static Func<A, R> Y<A, R>(Func<Func<A, R>, Func<A, R>> f)
        {
            Recursive2<A, R> rec = r => a => f(r(r))(a);
            return rec(rec);
        }

        // Using Y()
        private static void Test7()
        {
            Func<int, int> fact = Y<int, int>(f => n => (n == 1 ? 1 : n*f(n-1)));
            Console.WriteLine($"Wes Dyer: fact(6) = {fact(6)}"); // 720

            Func<int, int> fib = Y<int, int>(f => n => (n < 2 ? n : f(n-1) + f(n-2)));
            Console.WriteLine($"Wes Dyer: fib(6) = {fib(6)}"); // 8
        }
    }

    /// <summary>
    /// Enigmativity (StackOverflow)
    /// http://stackoverflow.com/questions/31819718/using-the-y-combinator-in-c-sharp/31821236#31821236
    /// </summary>
    class Enigmativity
    {
        public static void Test()
        {
            var fact = Y<int, int>(f => n => (n == 0 ? 1 : n*f(n-1)));
            Console.WriteLine($"Enigmativity: fact(6) = {fact(6)}"); // 720

            var fib = Y<int, int>(f => n => (n < 2 ? n : f(n-1) + f(n-2)));
            Console.WriteLine($"Enigmativity: fibo(6) = {fib(6)}"); // 8
        }

        delegate T S<T>(S<T> s);

        static T U<T>(S<T> s) => s(s);

        static Func<A, Z> Y<A, Z>(Func<Func<A, Z>, Func<A, Z>> f) =>
            U<Func<A, Z>>(r => a => f(U(r))(a));
    }

    /// <summary>
    /// Keith S (StackOverflow)
    /// http://stackoverflow.com/questions/4763690/alternative-y-combinator-definition/4765089#4765089
    /// </summary>
    class KeithS
    {
        public void Test()
        {
            // Usage: Factorial
            var curriedFactorial = YCombinator.Curry<int>((f, i) => i <= 0 ? 1 : f(f, i - 1) * i);
            // Assert.AreEqual(120, curriedFactorial(5));

            // Usage: Prime numbers
            var curriedPrime = YCombinator.Curry<int, List<int>>((p, i) => i == 1
                ? new List<int> { 2 }
                : p(p, i - 1).Concat(Enumerable.Range(p(p, i - 1)[i - 2], int.MaxValue - p(p, i - 1)[i - 2])
                    .First(x => p(p, i - 1).All(y => x%y != 0)).Yield()).ToList());
            // Assert.AreEqual(new[] { 2, 3, 5, 7, 11 }, curriedLambda(5));
        }
    }

    // Keith S
    public static class YCombinator
    {
        public delegate TOut RLambda<TIn, TOut>(RLambda<TIn, TOut> rLambda, TIn a);

        public static Func<T, T> Curry<T>(this RLambda<T, T> rLambda)
        {
            return a => rLambda(rLambda, a);
        }

        public static Func<TIn, TOut> Curry<TIn, TOut>(this RLambda<TIn, TOut> rLambda)
        {
            return a => rLambda(rLambda, a);
        }
    }

    public static class IEnumerableExt
    {
        /// <summary>
        /// Wraps this object instance into an IEnumerable&lt;T&gt; consisting of a single item.
        /// </summary>
        /// <typeparam name="T"> Type of the object. </typeparam>
        /// <param name="item"> The instance that will be wrapped. </param>
        /// <returns> An IEnumerable&lt;T&gt; consisting of a single item. </returns>
        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }
    }
}
