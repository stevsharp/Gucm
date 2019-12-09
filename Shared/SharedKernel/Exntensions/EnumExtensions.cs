using System;

namespace SharedKernel.Exntensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static int ToInt<TEnum>(this TEnum target) where TEnum : Enum
        {
            return (int)Enum.ToObject(target.GetType(), target);
        }

        public static TEnum ToEnum<TEnum>(this int target) where TEnum : Enum
        {
            return (TEnum)(object)target;
        }

    }
}
