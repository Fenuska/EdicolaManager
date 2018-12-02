using System.Globalization;

namespace Extensions.ExtensionMethods
{
    public static class ExtensionsToObjects
    {
        public static int ToInt(this object value)
        {
            int.TryParse(value.ToString(), NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out int result);
            return result;
        }

        public static decimal ToDecimal(this object value)
        {
            decimal.TryParse(value?.ToString(), NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out decimal result);
            return result;
        }
    }
}
