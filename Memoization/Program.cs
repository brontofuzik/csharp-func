using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Memoization
{
    class Program
    {
        static void Main(string[] args)
        {
            // No parameters.
            Memoize_NoParameters();

            // One parameter.
            Memoize_OneParameter_NonRecursive();
            Memoize_OneParameter_Recursive1();
            Memoize_OneParameter_Recursive2();

            // More than one parameter.
            Memoize_MoreParameters_NonRecursive();
            Memoize_MoreParameters_Recursive1();
            Memoize_MoreParameters_Recursive2();
        }

        private static void Memoize_NoParameters()
        {
            Console.Write("Memoize_NoParameters: ");

            // Define the original function.
            Func<string> helloWorld = () => "Hello World!";

            // Define the memoized function.
            Func<string> helloWorld_Memoized = Memoizer.Memoize_NoParameters(helloWorld);

            Console.WriteLine(helloWorld_Memoized());
        }

        /// <summary>
        /// One parameter. Non-recursive.
        /// </summary>
        private static void Memoize_OneParameter_NonRecursive()
        {
            Console.Write("Memoize_OneParameter_NonRecursive: ");

            // Define the original function.
            Func<double, double> sin = Math.Sin;

            // Define the memoized function.
            Func<double, double> sin_Memoized = Memoizer.Memoize_OneParameter(sin);

            Console.WriteLine(sin_Memoized(Math.PI / 2));
        }

        /// <summary>
        /// One parameter. Recursive. Version 1.
        /// </summary>
        private static void Memoize_OneParameter_Recursive1() 
        {
            Console.Write("Memoize_OneParameter_Recursive1: ");

            // Define the original function.
            Func<int, int> fibonacci = null;
            fibonacci = (n) => (n > 1) ? fibonacci(n - 1) + fibonacci(n - 2) : n;

            // Define the memoized function.
            Func<int, int> fibonacci_Memoized = Memoizer.Memoize_OneParameter(fibonacci);

            Console.WriteLine(fibonacci_Memoized(5));
        }

        /// <summary>
        /// One parameter. Recursive. Version 2.
        /// </summary>
        private static void Memoize_OneParameter_Recursive2()
        {
            Console.Write("Memoize_OneParameter_Recursive2: ");

            // Define the original function.
            Func<Func<int, int>, int, int> f = (fibonacci, n) => (n > 1) ? fibonacci(n - 1) + fibonacci(n - 2) : n;

            // Define the memoized function.
            Func<int, int> fibonacci_Memoized = Memoizer.Memoize_OneParameter_Recursive(f);

            Console.WriteLine(fibonacci_Memoized(5));
        }

        /// <summary>
        /// More than one parameter. Non-recursive.
        /// </summary>
        private static void Memoize_MoreParameters_NonRecursive()
        {
            Console.Write("Memoize_MoreParameters_NonRecursive: ");

            // Define the original function.
            Func<object[], object> multiply = (arguments) =>
            {
                // ----- Arguments -----
                int factor1 = (int)arguments[0];
                int factor2 = (int)arguments[1];
                // ---------------------

                return factor1 * factor2;
            };

            // Define the memoized function.
            Func<object[], object> multiply_Memoized = Memoizer.Memoize_MoreParameters(multiply);

            Console.WriteLine(multiply_Memoized(new object[] { 3, 4 }));
        }

        /// <summary>
        /// More than one parameter. Recursive. Version 1.
        /// </summary>
        private static void Memoize_MoreParameters_Recursive1()
        {
            Console.Write("Memoize_MoreParameters_Recursive1: ");

            // Define the original function.
            Func<object[], object> binomialCoefficient = null;
            binomialCoefficient = (arguments) =>
            {
                // ----- Arguments -----
                int n = (int)arguments[0];
                int k = (int)arguments[1];
                // ---------------------

                return (k != 0 && k != n) ? (int)binomialCoefficient(new object[] { n - 1, k }) +
                    (int)binomialCoefficient(new object[] { n - 1, k - 1 }) : 1;
            };

            // Define the memoized function.
            Func<object[], object> binomialCoefficient_Memoized = Memoizer.Memoize_MoreParameters(binomialCoefficient);

            Console.WriteLine(binomialCoefficient_Memoized(new object[] { 3, 2 }));
        }

        /// <summary>
        /// MMore than one parameter. Recursive. Version 2.
        /// </summary>
        private static void Memoize_MoreParameters_Recursive2()
        {
            Console.Write("Memoize_MoreParameters_Recursive1: ");

            // Define the original function.
            Func<Func<object[], object>, object[], object> f = (binomialCoefficient, arguments) =>
            {
                // ----- Arguments -----
                int n = (int)arguments[0];
                int k = (int)arguments[1];
                // ---------------------

                return (k != 0 && k != n) ? (int)binomialCoefficient(new object[] { n - 1, k }) +
                    (int)binomialCoefficient(new object[] { n - 1, k - 1 }) : 1;
            };

            // Define the memoized function.
            Func<object[], object> binomialCoefficient_Memoized = Memoizer.Memoize_MoreParameters_Recursive(f);

            Console.WriteLine(binomialCoefficient_Memoized(new object[] { 3, 2 }));
        }
    }
}
