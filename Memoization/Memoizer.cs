using System;
using System.Collections.Generic;
using System.Text;

public static class Memoizer
{
    public static Func<TResult> Memoize_NoParameters<TResult>(Func<TResult> f)
    {
        TResult cache = default(TResult);
        return () =>
        {
            if (cache == null)
            {
                cache = f();
            }
            return cache;
        };
    }

    public static Func<TArgument, TResult> Memoize_OneParameter<TArgument, TResult>(
        Func<TArgument, TResult> f)
    {
        var cache = new Dictionary<TArgument, TResult>();
        Func<TArgument, TResult> memoizedFunction = null;
        memoizedFunction = (argument) =>
        {
            if (!cache.ContainsKey(argument))
            {
                cache[argument] = f(argument);
            }
            return cache[argument];
        };
        return memoizedFunction;
    }

    public static Func<TArgument, TResult> Memoize_OneParameter_Recursive<TArgument, TResult>(
        Func<Func<TArgument, TResult>, TArgument, TResult> f)
    {
        var cache = new Dictionary<TArgument, TResult>();
        Func<TArgument, TResult> memoizedFunction = null;
        memoizedFunction = (argument) =>
        {
            if (!cache.ContainsKey(argument))
            {
                cache[argument] = f(memoizedFunction, argument);
            }
            return cache[argument];
        };
        return memoizedFunction;
    }

    public static Func<object[], object> Memoize_MoreParameters(
        Func<object[], object> f)
    {
        var cache = new Dictionary<string, object>();
        Func<object[], object> memoizedFunction = null;
        memoizedFunction = (arguments) =>
        {
            string argumentString = CreateArgumentString(arguments);
            if (!cache.ContainsKey(argumentString))
            {
                cache[argumentString] = f(arguments);
            }
            return cache[argumentString];
        };
        return memoizedFunction;
    }

    public static Func<object[], object> Memoize_MoreParameters_Recursive(
        Func<Func<object[], object>, object[], object> f)
    {
        var cache = new Dictionary<string, object>();
        Func<object[], object> memoizedFunction = null;
        memoizedFunction = (arguments) =>
        {
            string argumentString = CreateArgumentString(arguments);
            if (!cache.ContainsKey(argumentString))
            {
                cache[argumentString] = f(memoizedFunction, arguments);
            }
            return cache[argumentString];
        };
        return memoizedFunction;
    }

    // ----- PRIVATE -----

    private static string CreateArgumentString(object[] arguments)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (object argument in arguments)
        {
            stringBuilder.Append(argument.GetHashCode().ToString());
            stringBuilder.Append("|");
        }
        return stringBuilder.ToString();
    }
}