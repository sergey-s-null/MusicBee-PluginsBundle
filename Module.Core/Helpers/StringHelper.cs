namespace Module.Core.Helpers
{
    public static class StringHelper
    {
        public static string Capitalize(this string value)
        {
            return value.Length switch
            {
                0 => value,
                1 => value.ToUpper(),
                _ => char.ToUpper(value[0]) + value.Substring(1)
            };
        }
    }
}