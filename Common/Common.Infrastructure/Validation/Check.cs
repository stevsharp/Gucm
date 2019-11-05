using System;

namespace Common.Infrastructure.Validation
{
    public sealed class Check
    {
        public static void That(bool assertion, Action exceptionToThrow)
        {
            if (!assertion)
                exceptionToThrow.Invoke();
        }

        public static void IsNotNull(Object value, Action exceptionToThrow)
        {
            That(value != null, exceptionToThrow);
        }
        public static void ArgumentIsNotNull(Object value, string param) => That(value != null, () => throw new ArgumentNullException(param));

        public static void ArgumentIsGreaterThanZero(decimal value, string param) => That(value > 0, () => throw new ArgumentException(param));

        public static void IsNull(Object value, Action exceptionToThrow)
        {
            That(value == null, exceptionToThrow);
        }

        public static void ThatIsNotAnEmptyString(string value, Action exceptionToThrow)
        {
            That(!String.IsNullOrEmpty(value) && !String.IsNullOrWhiteSpace(value), exceptionToThrow);
        }

        public static void IsGreaterThan(int numberToCompare, int quantity, Action exceptionToThrow)
        {
            That(quantity >= numberToCompare, exceptionToThrow);
        }

    }
}
