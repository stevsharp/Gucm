using System;
using System.Collections.Generic;
using System.Globalization;

namespace SharedKernel.Exntensions
{
    public static class NumericExtensions
    {
        public static int AsInt(this object item, int defaultInt = default(int))
        {
            if (item == null)
                return defaultInt;

            int result;
            if (!int.TryParse(item.ToString(), out result))
                return defaultInt;

            return result;
        }

        public static double AsDouble(this object item, double defaultDouble = default(double))
        {
            if (item == null)
                return defaultDouble;

            double result;
            if (!double.TryParse(item.ToString(), out result))
                return defaultDouble;

            return result;
        }

        public static double AsDouble(this object item, CultureInfo cultureInfo, double defaultDouble = default(double))
        {
            if (item == null)
                return defaultDouble;

            var style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            double result;
            if (!double.TryParse(item.ToString(), style, cultureInfo, out result))
                return defaultDouble;

            return result;
        }

        public static Decimal AsDecimal(this object item, decimal defaultDecimal = default(decimal))
        {
            if (item == null)
                return defaultDecimal;

            Decimal result;

            if (!Decimal.TryParse(item.ToString(), out result))
                return defaultDecimal;

            return result;
        }

        public static Decimal AsDecimal(this object item, CultureInfo cultureInfo, decimal defaultDecimal = default(decimal))
        {
            if (item == null)
                return defaultDecimal;

            var style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            Decimal result;

            if (!Decimal.TryParse(item.ToString(), style, cultureInfo, out result))
                return defaultDecimal;

            return result;
        }

        public static Decimal AsDecimal(this object item, CultureInfo cultureInfo, out bool hasErrors, decimal defaultDecimal = default(decimal))
        {
            hasErrors = false;
            if (item == null)
                return defaultDecimal;

            var style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            Decimal result;

            Decimal.Parse(item.ToString(), style, cultureInfo);

            if (!Decimal.TryParse(item.ToString(), style, cultureInfo, out result))
            {
                hasErrors = true;
                return defaultDecimal;
            }

            return result;
        }

        public static Double AsDouble(this object item, CultureInfo cultureInfo, out bool hasErrors, double defaultDouble = default(double))
        {
            hasErrors = false;
            if (item == null)
                return defaultDouble;

            var style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            Double result;

            if (!Double.TryParse(item.ToString(), style, cultureInfo, out result))
            {
                hasErrors = true;
                return defaultDouble;
            }

            return result;
        }

        public static Decimal ToMoney(this Decimal @this)
        {
            return Math.Round(@this, 2);
        }

        public static Decimal ToMoney(this Decimal @this, int places)
        {
            return Math.Round(@this, places);
        }

        public static string DelimetedIntAsString(this IEnumerable<int> collectionOfStrings, char separator)
        {
            return String.Join(separator, collectionOfStrings);
        }
    }
}
