using System;
using System.Collections.Generic;
using System.Linq;


namespace SharedKernel.Exntensions
{
    public static class StringExtensions
    {
        public static string AsBase64String(this object item)
        {
            if (item == null)
                return null;
            return Convert.ToBase64String((byte[])item);
        }

        public static byte[] AsByteArray(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            return Convert.FromBase64String(s);
        }

        public static bool IsNullOrEmpty(this String s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNullOrWhiteSpace(this String s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static string DelimetedString(this IEnumerable<string> collectionOfStrings, char separator)
        {
            if (collectionOfStrings.Count() > 0)
                return String.Join(separator, collectionOfStrings);

            return string.Empty;
        }

        /// <summary>
        /// How to use 
        /// bool areEqual = a.EqualsByValue(b);
        /// The default StringComparison is InvariantCultureIgnoreCase
        /// </summary>
        /// <param name="inString"></param>
        /// <param name="compared"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static bool EqualsByValue(this string inString, string compared, StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase)
        {
            if (string.IsNullOrEmpty(inString) && string.IsNullOrEmpty(compared))
                return true;

            if (inString == null)
                return false;

            return inString.Equals(compared, stringComparison);
        }
    }
}
