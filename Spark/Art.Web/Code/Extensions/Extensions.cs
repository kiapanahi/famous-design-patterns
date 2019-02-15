using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art.Web
{
    // small set of helpful extension methods

    public static class Extensions
    {
        // comma separate an enumerable source

        public static string CommaSeparate<T, U>(this IEnumerable<T> source, Func<T, U> func)
        {
            return string.Join(",", source.Select(s => func(s).ToString()).ToArray());
        }

        // comma separate an enumerable source

        public static string CommaSeparatePadded<T, U>(this IEnumerable<T> source, Func<T, U> func)
        {
            return string.Join(", ", source.Select(s => func(s).ToString()).ToArray());
        }

        // checks if string is numeric

        public static bool IsNumeric(this string s)
        {
            int result;
            return int.TryParse(s, out result);
        }

        // ** Iterator Pattern
        // foreach iterates over an enumerable collection

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        // truncates a string and appends ellipsis if beyond a given length

        public static string Ellipsify(this string s, int maxLength)
        {
            if (string.IsNullOrEmpty(s)) return "";

            if (s.Length <= maxLength) return s;

            return s.Substring(0, maxLength) + "...";
        }
    }
}