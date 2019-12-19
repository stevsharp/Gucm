namespace SharedKernel.Exntensions
{
    public static class BoolExtensions
    {
        public static bool AsBool(this object item, bool defaultBool = default)
        {
            if (item == null)
                return defaultBool;

            bool bValue;

            if (bool.TryParse(item.ToString(), out bValue))
            {
                return bValue;
            }

            return false;
        }

        public static bool AsBoolFromInt(this object item, bool defaultBool = default)
        {
            if (item == null)
                return defaultBool;

            int bValue = 0;

            if (int.TryParse(item.ToString(), out bValue))
            {
                if (bValue == 1)
                    return true;
            }

            return false;
        }

    }
}
