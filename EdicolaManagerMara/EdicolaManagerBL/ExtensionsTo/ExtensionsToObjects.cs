namespace EdicolaManager.ExtensionMethods
{
    public static class ExtensionsToObjects
    {
        public static int ToInt(this object value)
        {
            int.TryParse(value.ToString(), out int result);
            return result;
        }
    }
}
