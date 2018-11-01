namespace EdicolaManager.ExtensionMethods
{
    public static class ExtensionsToObjects
    {
        public static int ToInt(this object value)
        {
            int result;
            try
            {
                int.TryParse(value?.ToString(), out result);                    
            }
            catch
            {
                result = 0;
            }
            return result;
        }
    }
}
