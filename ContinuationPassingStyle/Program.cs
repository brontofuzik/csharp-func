using System;

namespace ContinuationPassingStyle
{
    class Program
    {
        static void Main(string[] args)
        {
            Example1();
            Example2();
            Example3();
            Example4();
        }

        #region Example 1

        private static void Example1()
        {
            // Non-CPS
            Console.WriteLine(Identity("foo"));

            // CPS
            Identity("foo", s => Console.WriteLine(s));
        }

        // Non-CPS
        static T Identity<T>(T value)
        {
            return value;
        }

        // CPS
        static void Identity<T>(T value, Action<T> k)
        {
            k(value);
        }

        #endregion

        #region Example 2

        private static void Example2()
        {
            // Non-CPS
            Console.WriteLine(Max(3, 4)); 

            // CPS
            Max(3, 4, x => Console.WriteLine(x));
        }

        // Non-CPS
        static int Max(int n, int m)
        {
            if (n > m)
            {
                return n;
            }
            else
            {
                return m;
            }
        }

        // CPS
        static void Max(int n, int m, Action<int> k)
        {
            if (n > m)
            {
                k(n);
            }
            else
            {
                k(m);
            }
        }

        #endregion

        #region Example 3

        private static void Example3()
        {
            // Non-CPS
            Console.WriteLine(F(1) + 1);

            // CPS
            F(1, x => Console.WriteLine(x + 1));
        }

        // Non-CPS
        static int F(int n)
        {
            return G(n + 1) + 1;
        }

        // Non-CPS
        static int G(int n)
        {
            return n + 1;
        }

        // CPS
        static void F(int n, Action<int> k)
        {
            G(n + 1, x => k(x + 1));
        }

        // CPS
        static void G(int n, Action<int> k)
        {
            k(n + 1);
        }

        #endregion

        #region Example 4

        private static void Example4()
        {
            // Non-CPS
            Console.WriteLine(Factorial(5));

            // CPS
            Factorial(5, x => Console.WriteLine(x));
        }

        // Non-CPS
        private static int Factorial(int n)
        {
            if (n == 0)
            {
                return 1;
            }
            else
            {
                return n * Factorial(n - 1);
            }
        }

        // CPS
        static void Factorial(int n, Action<int> k)
        {
            if (n == 0)
            {
                k(1);
            }
            else
            {
                Factorial(n - 1, x => k(n * x));
            }
        }

        #endregion
    }
}
