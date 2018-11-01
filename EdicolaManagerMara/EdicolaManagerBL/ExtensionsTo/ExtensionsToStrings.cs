namespace EdicolaManager.ExtensionMethods
{
    public static class ExtensionsToStrings
    {
        public static int ToInt(this string value)
        {
            int result;
            try
            {
                int.TryParse(value, out result);
            }
            catch
            {
                result = 0;
            }
            return result;
        }

        public static decimal ToDecimal(this string value)
        {
            decimal result;
            try
            {
                decimal.TryParse(value, out result);
            }
            catch { result = 0; }
            return result;
        }
    }
}
