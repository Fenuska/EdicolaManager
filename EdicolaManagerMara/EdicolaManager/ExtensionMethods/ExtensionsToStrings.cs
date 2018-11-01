namespace EdicolaManager.ExtensionMethods
{
    public static class ExtensionsToStrings
    {
        public static int ToInt(this string value)
        {
            int.TryParse(value, out int result);
            return result;
        }

        public static decimal ToDecimal(this string value)
        {
            decimal.TryParse(value, out decimal result);
            return result;
        }

        public static string RemoveSpaces(this string value)
        {
            return value.Trim();
        }
    }
}
