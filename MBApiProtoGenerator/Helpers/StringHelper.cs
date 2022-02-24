using System.Text;

namespace MBApiProtoGenerator.Helpers
{
    public static class StringHelper
    {
        public static string Indented(this string value, int indentLevel = 1, string indent = "    ")
        {
            var builder = new StringBuilder();
            for (var i = 0; i < indentLevel; i++)
            {
                builder.Append(indent);
            }

            builder.Append(value);

            return builder.ToString();
        }
    }
}